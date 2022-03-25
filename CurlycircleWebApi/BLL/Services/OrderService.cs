using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(
          IOrderRepository orderRepository,
          IUnitOfWork unitOfWork,
          IMapper mapper)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EntityCreatedViewModel> CreateOrderAsync(OrderUpsertDto orderUpsertDto)
        {
            var order = _mapper.Map<Order>(orderUpsertDto);

            var id = _orderRepository.AddOrder(order);
            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(id);
        }

        public async Task<OrdersViewModel> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var ordersViewModel = _mapper.Map<OrdersViewModel>(orders);
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

        public async Task<EntityCreatedViewModel> AddOrderItemAsync(int orderId, OrderItemUpsertDto orderItemCreateDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            var orderItem = _mapper.Map<OrderItem>(orderItemCreateDto);
            order.AddOrderItem(orderItem);

            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(orderItem.Id);
        }
    }
}
