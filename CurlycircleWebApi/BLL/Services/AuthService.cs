﻿using AutoMapper;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;

        private IConfiguration Configuration { get; }

        public AuthService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, ICartRepository cartRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Configuration = configuration;
            _cartRepository = cartRepository;
        }

        public async Task<UserViewModel> LoginAsync(LoginDto loginDto)
        {
            var user = await FindUserByEmailAsync(loginDto.Email);
            var passwordMatches = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!passwordMatches)
            {
                throw new ValidationAppException("Login attempt failed.", new[]
                {
                    "Invalid user credentials."
                });
            }

            var accessToken = await CreateAccessTokenAsync(user);
            var refreshToken = CreateRefreshToken();
            user.RefreshToken = refreshToken;


            var userRoles = await _userManager.GetRolesAsync(user);
            var role = Role.User;

            foreach (var userRole in userRoles)
            {
                if (userRole.Equals("Admin"))
                {
                    role = Role.Admin;
                }
            }

            user.Cart = await _cartRepository.GetUserCartAsync(user.Id);

            if (loginDto.CartId.HasValue && loginDto.CartId != user.Cart.Id)
            {
                var cartBeforeLogin = await _cartRepository.GetCartByIdAsync(loginDto.CartId.GetValueOrDefault());

                foreach (var oldCartItem in cartBeforeLogin.CartItems)
                {
                    await _cartRepository.AddCartItemAsync(user.Cart.Id, oldCartItem);
                }

                await _cartRepository.DeleteCartAsync(cartBeforeLogin.Id);
            }

            await _unitOfWork.SaveChangesAsync();

            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.Role = role;
            userViewModel.AccessToken = accessToken;
            userViewModel.RefreshToken = refreshToken;

            return userViewModel;
        }

        public async Task<EntityCreatedViewModel> RegisterAsync(RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userExists != null)
            {
                throw new ValidationAppException("Register attempt failed.", new[]
                {
                    "User with given email already exists."
                });
            }

            ApplicationUser user = _mapper.Map<ApplicationUser>(registerDto);
            user.Cart = new Cart();
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new ValidationAppException("User registration failed.", result.Errors.Select(ent => ent.Description));
            }

            //Handle roles
            var roleResult = await _userManager.AddToRoleAsync(user, "User");

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

            if (user.RefreshToken != refreshToken)
            {
                throw new NoAccessException("Refresh attempt failed.", new[]
                {
                    "Refresh tokens do not match."
                });
            }
            else if (user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new NoAccessException("Refresh attempt failed.", new[]
                {
                    "Refresh token is expired."
                });
            }

            var newAccessToken = await CreateAccessTokenAsync(user);
            var newRefreshToken = CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new ValidationAppException("Refresh failed.", result.Errors.Select(ent => ent.Description));
            }
            await _unitOfWork.SaveChangesAsync();

            return new TokenViewModel()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task RevokeAsync(RevokeDto revokeDto)
        {
            var user = await FindUserByIdAsync(revokeDto.Id);
            user.RefreshToken = null;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDataViewModel> UpdateUserAsync(UserUpdateDto userUpdateDto)
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

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDataViewModel>(user);
        }

        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var user = await FindUserByEmailAsync(changePasswordDto.Email);
            var passwordMatches = await _userManager.CheckPasswordAsync(user, changePasswordDto.OldPassword);
            if (!passwordMatches)
            {
                throw new ValidationAppException("Login attempt failed.", new[]
                {
                    "Invalid user credentials."
                });
            }
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                throw new ValidationAppException("Change password failed.", result.Errors.Select(ent => ent.Description));
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(DeleteUserDto deleteUserDto)
        {
            var user = await FindUserByIdAsync(deleteUserDto.Id);
            var passwordMatches = await _userManager.CheckPasswordAsync(user, deleteUserDto.Password);
            if (!passwordMatches)
            {
                throw new ValidationAppException("Delete attempt failed.", new[]
                {
                    "Invalid user credentials."
                });
            }
            await _userManager.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDataViewModel> GetUserDataAsync(int userId)
        {
            var user = await FindUserByIdAsync(userId);

            return _mapper.Map<UserDataViewModel>(user);
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
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(Configuration["Jwt:TokenValidityInMinutes"]));

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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SigningKey"])),
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
