using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [AllowAnonymous]
        public Task<TokenViewModel> Login([FromBody] LoginDto loginDto)
        {
            return authService.LoginAsync(loginDto);
        }
    }
}
