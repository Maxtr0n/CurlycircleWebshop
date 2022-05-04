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

        public UserController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}/orders")]
        [Authorize]
        public Task<OrdersViewModel> GetUserOrders([FromRoute] int userId)
        {
            return _orderService.GetUserOrders(userId);
        }

    }
}
