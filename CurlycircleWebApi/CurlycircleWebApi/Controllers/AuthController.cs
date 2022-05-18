using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public Task<UserViewModel> Login([FromBody] LoginDto loginDto)
        {
            return authService.LoginAsync(loginDto);
        }

        [HttpPost("register")]
        public Task<EntityCreatedViewModel> Register([FromBody] RegisterDto registerDto)
        {
            return authService.RegisterAsync(registerDto);
        }

        [HttpPost("refresh")]
        public Task<TokenViewModel> Refresh(RefreshDto refreshDto)
        {
            return authService.RefreshAsync(refreshDto);
        }

        [HttpPost("revoke")]
        [Authorize]
        public Task Revoke(RevokeDto revokeDto)
        {
            return authService.RevokeAsync(revokeDto);
        }

        [HttpPut("update")]
        [Authorize]
        public Task<UserDataViewModel> Update(UserUpdateDto userUpdateDto)
        {
            return authService.UpdateUserAsync(userUpdateDto);
        }

        [HttpPut("change-password")]
        [Authorize]
        public Task ChangePassword(ChangePasswordDto changePasswordDto)
        {
            return authService.ChangePasswordAsync(changePasswordDto);
        }

        [HttpDelete("delete-user")]
        [Authorize]
        public Task DeleteUser(DeleteUserDto deleteUserDto)
        {
            return authService.DeleteUserAsync(deleteUserDto);
        }

        [HttpGet("user-data")]
        [Authorize]
        public Task<UserDataViewModel> GetUserData(int userId)
        {
            return authService.GetUserDataAsync(userId);
        }
    }
}
