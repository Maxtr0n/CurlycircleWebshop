using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderItemService(
          IOrderItemRepository orderItemRepository,
          IUnitOfWork unitOfWork,
          IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<EntityCreatedViewModel> CreateOrderItemAsync(OrderItemUpsertDto orderItemCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItemViewModel> FindOrderItemByIdAsync(int orderItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderItemAsync(int orderItemId, OrderItemUpsertDto orderItemUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderItemAsync(int orderItemId)
        {
            throw new NotImplementedException();
        }
    }
}
