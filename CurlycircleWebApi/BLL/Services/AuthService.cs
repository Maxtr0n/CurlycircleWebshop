using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private IConfiguration Configuration { get; }

        public AuthService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Configuration = configuration;
        }

        public async Task<UserViewModel> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await FindUserAsync(loginDto.Username);
                var passwordMatches = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (!passwordMatches)
                {
                    throw new Exception();
                }
                var token = new TokenViewModel
                {
                    AccessToken = await CreateAccessTokenAsync(user)
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

                return new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
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

        private async Task<ApplicationUser> FindUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new EntityNotFoundException($"User with username '{username}' not found.");
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
    }
}
