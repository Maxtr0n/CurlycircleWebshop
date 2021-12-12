using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurlycircleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class OrderItemsController : ApiController
    {
        private readonly IIdentityHelper _identityHelper;
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IIdentityHelper identityHelper, IOrderItemService orderItemCategoryService, IOrderItemService orderItemService)
        {
            _identityHelper = identityHelper;
            _orderItemService = orderItemService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<EntityCreatedViewModel> CreateOrderItem([FromBody] OrderItemUpsertDto orderItemCreateDto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return _orderItemService.CreateOrderItemAsync(orderItemCreateDto);
        }

        [HttpGet("{orderItemId}")]
        [Authorize(Roles = "Admin")]
        public Task<OrderItemViewModel> GetOrderItemById([FromRoute] int orderItemId)
        {
            return _orderItemService.FindOrderItemByIdAsync(orderItemId);
        }

        [HttpPut("{orderItemId}")]
        [Authorize(Roles = "Admin")]
        public Task UpdateOrderItem([FromRoute] int orderItemId, [FromBody] OrderItemUpsertDto orderItemUpdateDto)
        {
            return _orderItemService.UpdateOrderItemAsync(orderItemId, orderItemUpdateDto);
        }

        [HttpDelete("{orderItemId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task DeleteOrderItem([FromRoute] int orderItemId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _orderItemService.DeleteOrderItemAsync(orderItemId);
        }
    }
}
