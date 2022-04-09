using AutoMapper;
using BLL.Dtos;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private IConfiguration Configuration { get; }

        public AuthService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, ICartService cartService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Configuration = configuration;
            _cartService = cartService;
        }

        public async Task<UserViewModel> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await FindUserByEmailAsync(loginDto.Email);
                var passwordMatches = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (!passwordMatches)
                {
                    throw new Exception();
                }
                var token = new TokenViewModel
                {
                    AccessToken = await CreateAccessTokenAsync(user),
                    RefreshToken = CreateRefreshToken()
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                var role = Role.User;
                foreach (var userRole in userRoles)
                {
                    if (userRole.Equals("Admin"))
                    {
                        role = Role.Admin;
                    }
                }

                if (loginDto.CartId.HasValue && loginDto.CartId != user.Cart.Id)
                {
                    var cartBeforeLogin = await _cartService.FindCartByIdAsync(loginDto.CartId.GetValueOrDefault());
                    foreach (var oldCartItem in cartBeforeLogin.CartItems)
                    {
                        var cartItemCreateDto = _mapper.Map<CartItemUpsertDto>(oldCartItem);
                        await _cartService.AddCartItemAsync(user.Cart.Id, cartItemCreateDto);
                    }

                    await _cartService.DeleteCartAsync(cartBeforeLogin.Id);
                }

                return new UserViewModel
                {
                    Id = user.Id,
                    CartId = user.Cart.Id,
                    Email = user.Email,
                    Role = role,
                    Token = token
                };
            }
            catch (Exception)
            {
                throw new ValidationAppException("Login attempt failed.", new[]
                {
                    "Invalid user credentials."
                });
            }
        }

        public async Task<EntityCreatedViewModel> RegisterAsync(RegisterDto registerDto)
        {
            var userExists = await FindUserByEmailAsync(registerDto.Email);
            if (userExists != null)
            {
                throw new ValidationAppException("Register attempt failed.", new[]
                {
                    "User with given email already exists."
                });
            }
            ApplicationUser user = _mapper.Map<ApplicationUser>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new ValidationAppException("User registration failed.", result.Errors.Select(ent => ent.Description));
            }

            await _unitOfWork.SaveChangesAsync();

            return new EntityCreatedViewModel
            {
                Id = user.Id
            };
        }

        public async Task<TokenViewModel> RefreshAsync(RefreshDto refreshDto)
        {
            var principal = GetPrincipalFromExpiredtoken(refreshDto.AccessToken);
            var user = await FindUserByIdAsync(refreshDto.Id);
            var refreshToken = refreshDto.RefreshToken;
            var accessToken = refreshDto.AccessToken;

            if (user == null)
            {
                throw new ValidationAppException("Refresh attempt failed.", new[]
                {
                    "User does not exist."
                });
            }
            else if (user.RefreshToken != refreshToken)
            {
                throw new ValidationAppException("Refresh attempt failed.", new[]
                {
                    "Refresh tokens do not match."
                });
            }
            else if (user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new ValidationAppException("Refresh attempt failed.", new[]
                {
                    "Refresh token is expired."
                });
            }

            var newAccessToken = await CreateAccessTokenAsync(user);
            var newRefreshToken = CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _unitOfWork.SaveChangesAsync();

            return new TokenViewModel()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task RevokeAsync(RevokeDto revokeDto)
        {
            var user = await FindUserByEmailAsync(revokeDto.Email);

            if (user == null)
            {
                throw new ValidationAppException("Revoke attempt failed.", new[]
                {
                    "User does not exist."
                });
            }

            user.RefreshToken = null;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await FindUserByIdAsync(userUpdateDto.UserId);
            if (userUpdateDto.Email is not null)
            {
                var userWithEmail = await FindUserByEmailAsync(userUpdateDto.Email);
                if (userWithEmail is not null)
                {
                    throw new ValidationAppException("Update user failed.", new[]
                    {
                        "Email is already taken."
                    });
                }

                user.Email = userUpdateDto.Email;
                user.UserName = userUpdateDto.Email;
            }

            user.FirstName = userUpdateDto.FirstName;
            user.LastName = userUpdateDto.LastName;
            user.PhoneNumber = userUpdateDto.PhoneNumber;
            user.Address.City = userUpdateDto.City;
            user.Address.ZipCode = userUpdateDto.ZipCode;
            user.Address.Line1 = userUpdateDto.Line1;
            user.Address.Line2 = userUpdateDto.Line2;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new ValidationAppException("Update user failed.", result.Errors.Select(ent => ent.Description));
            }
        }

        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var user = await FindUserByEmailAsync(changePasswordDto.Email);
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                throw new ValidationAppException("Change password failed.", result.Errors.Select(ent => ent.Description));
            }
        }

        private async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new EntityNotFoundException($"User with email '{email}' not found.");
            }
            return user;
        }

        private async Task<ApplicationUser> FindUserByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new EntityNotFoundException($"User with ID '{id}' not found.");
            }
            return user;
        }

        private async Task<string> CreateAccessTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SigningKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(Configuration["Jwt:ExpirationMinutes"]));

            var token = new JwtSecurityToken(
                Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredtoken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
