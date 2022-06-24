using BLL.Interfaces;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public UserController(IOrderService orderService, IAuthService authService)
        {
            _orderService = orderService;
            _authService = authService;
        }

        [HttpGet("{userId}/orders")]
        [Authorize]
        public Task<OrdersViewModel> GetUserOrders([FromRoute] int userId)
        {
            return _orderService.GetUserOrders(userId);
        }

        [HttpGet("{userId}/user-data")]
        [Authorize]
        public Task<UserDataViewModel> GetUserData([FromRoute] int userId)
        {
            return _authService.GetUserDataAsync(userId);
        }

    }
}
