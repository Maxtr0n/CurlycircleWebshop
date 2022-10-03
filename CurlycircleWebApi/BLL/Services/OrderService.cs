using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.QueryParameters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBarionClient _barionClient;

        private IConfiguration Configuration { get; }

        public OrderService(
          IOrderRepository orderRepository,
          ICartRepository cartRepository,
          IUnitOfWork unitOfWork,
          IMapper mapper,
          IConfiguration configuration,
          IBarionClient barionClient
          )
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Configuration = configuration;
            _barionClient = barionClient;
        }

        public async Task<EntityCreatedViewModel> CreateOrderAsync(OrderUpsertDto orderUpsertDto)
        {
            var order = _mapper.Map<Order>(orderUpsertDto);

            if (orderUpsertDto.PaymentMethod == PaymentMethod.WebPayment)
            {
                HandleWebPayment(orderUpsertDto);
            }

            var userCart = await _cartRepository.GetCartByIdAsync(orderUpsertDto.CartId);
            order.OrderDateTime = DateTime.Now;
            order.Total = 0;

            foreach (var cartItem in userCart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                };
                order.OrderItems.Add(orderItem);
                order.Total += cartItem.Quantity * cartItem.Price;
            }

            var id = _orderRepository.AddOrder(order);
            userCart.ClearCart();

            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(id);
        }

        private bool HandleWebPayment(OrderUpsertDto orderUpsertDto)
        {


            return true;
        }

        public async Task<PagedOrdersViewModel> GetAllOrdersAsync(OrderQueryParameters orderQueryParameters)
        {
            var orders = await _orderRepository.GetAllAsync(orderQueryParameters);

            var ordersViewModel = _mapper.Map<PagedOrdersViewModel>(orders);
            return ordersViewModel;
        }

        public async Task<OrderViewModel> FindOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            return orderViewModel;
        }

        public async Task UpdateOrderAsync(int orderId, OrderUpsertDto orderUpdateDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            //order.Update(_mapper.Map<Order>(orderUpdateDto));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<OrderItemsViewModel> GetAllOrderOrderItemsAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            var orderItemsViewModel = _mapper.Map<OrderItemsViewModel>(order.OrderItems);
            return orderItemsViewModel;
        }

        public async Task<OrdersViewModel> GetUserOrders(int userId)
        {
            var orders = await _orderRepository.GetUserOrdersAsync(userId);
            var ordersViewModel = _mapper.Map<OrdersViewModel>(orders);
            return ordersViewModel;
        }
    }
}
