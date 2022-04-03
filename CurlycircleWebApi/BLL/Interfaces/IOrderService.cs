using BLL.Dtos;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<EntityCreatedViewModel> CreateOrderAsync(OrderUpsertDto orderUpsertDto);

        Task<OrdersViewModel> GetAllOrdersAsync();

        Task<OrderViewModel> FindOrderByIdAsync(int orderId);

        Task UpdateOrderAsync(int orderId, OrderUpsertDto orderUpdateDto);

        Task DeleteOrderAsync(int orderId);

        Task<OrderItemsViewModel> GetAllOrderOrderItemsAsync(int orderId);

        Task<EntityCreatedViewModel> AddOrderItemAsync(int orderId, OrderItemUpsertDto orderItemCreateDto);
    }
}
