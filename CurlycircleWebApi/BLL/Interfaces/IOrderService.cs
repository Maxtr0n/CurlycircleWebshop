using BLL.Dtos;
using BLL.ViewModels;
using Domain.Entities.QueryParameters;
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

        Task<PagedOrdersViewModel> GetAllOrdersAsync(OrderQueryParameters orderQueryParameters);

        Task<OrderViewModel> FindOrderByIdAsync(int orderId);

        Task UpdateOrderAsync(int orderId, OrderUpsertDto orderUpdateDto);

        Task DeleteOrderAsync(int orderId);

        Task<OrderItemsViewModel> GetAllOrderOrderItemsAsync(int orderId);

        Task<OrdersViewModel> GetUserOrders(int userId);

    }
}
