using Domain.Entities;
using Domain.Entities.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        int AddOrder(Order order);

        Task<PagedList<Order>> GetAllAsync(OrderQueryParameters orderQueryParameters);

        Task<Order> GetOrderByIdAsync(int orderId);

        void UpdateOrder(Order order);

        Task DeleteOrderAsync(int orderId);

        Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
    }
}
