﻿using BLL.Dtos;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task<UserViewModel> LoginAsync(LoginDto loginDto);

        Task<EntityCreatedViewModel> RegisterAsync(RegisterDto registerDto);

        Task<TokenViewModel> RefreshAsync(RefreshDto refreshDto);

        Task RevokeAsync(RevokeDto revokeDto);

        Task<UserDataViewModel> UpdateUserAsync(UserUpdateDto userUpdateDto);

        Task ChangePasswordAsync(ChangePasswordDto changePasswordDto);

        Task DeleteUserAsync(DeleteUserDto deleteUserDto);

        Task<UserDataViewModel> GetUserDataAsync(int userId);
    }
}
