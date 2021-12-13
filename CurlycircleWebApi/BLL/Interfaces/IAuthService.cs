using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dtos;
using BLL.ViewModels;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task<TokenViewModel> LoginAsync(LoginDto loginDto);

        //Task<EntityCreatedViewModel> CreateUserAsync(CreateUserDto createUserDto);
    }
}
