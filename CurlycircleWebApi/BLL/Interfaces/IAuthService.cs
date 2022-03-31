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

        Task RegisterAsync(RegisterDto registerDto);

        Task<TokenViewModel> Refresh(RefreshDto refreshDto);

        Task Revoke(RevokeDto revokeDto);
    }
}
