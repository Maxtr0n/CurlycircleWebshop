using BLL.Dtos;
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

        Task RegisterAsync(UserUpsertDto registerDto);

        Task<TokenViewModel> RefreshAsync(RefreshDto refreshDto);

        Task RevokeAsync(RevokeDto revokeDto);

        Task UpdateUserAsync(UserUpsertDto updateDto);

        Task ChangePasswordAsync(ChangePasswordDto changePasswordDto);

        Task<EntityCreatedViewModel> CreateAnonymousUserAsync();
    }
}
