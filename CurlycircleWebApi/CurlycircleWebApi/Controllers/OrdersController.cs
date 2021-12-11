﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CurlycircleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : ApiController
    {
        private readonly IIdentityHelper _identityHelper;
        private readonly IOrderService _orderService;

        public OrdersController(IIdentityHelper identityHelper, IOrderService orderService, IOrderItemService orderItemService)
        {
            _identityHelper = identityHelper;
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<EntityCreatedViewModel> CreateOrder([FromBody] OrderUpsertDto orderCreateDto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return _orderService.CreateOrderAsync(orderCreateDto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public Task<OrdersViewModel> GetOrders()
        {
            return _orderService.GetAllOrdersAsync();
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles = "Admin")]
        public Task<OrderViewModel> GetOrderById([FromRoute] int orderId)
        {
            return _orderService.FindOrderByIdAsync(orderId);
        }

        [HttpPut("{orderId}")]
        [Authorize(Roles = "Admin")]
        public Task UpdateOrder([FromRoute] int orderId, [FromBody] OrderUpsertDto orderUpdateDto)
        {
            return _orderService.UpdateOrderAsync(orderId, orderUpdateDto);
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task DeleteOrder([FromRoute] int orderId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _orderService.DeleteOrderAsync(orderId);
        }

        [HttpPost("{orderId}/orderItem")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<EntityCreatedViewModel> AddOrderItem([FromRoute] int orderId, [FromBody] OrderItemUpsertDto orderItemUpsertDto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return _orderService.AddOrderItemAsync(orderId, orderItemUpsertDto);
        }

        [HttpGet("{orderId}/orderItem")]
        [Authorize(Roles = "Admin")]
        public Task<OrderItemsViewModel> GetOrderItems([FromRoute] int orderId)
        {
            return _orderService.GetAllOrderOrderItemsAsync(orderId);
        }
    }
}
