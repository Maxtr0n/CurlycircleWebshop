using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderItemRepository
    {
        int AddOrderItemItem(OrderItem orderItem);

        Task<OrderItem> GetOrderItemByIdAsync(int orderItemId);

        void UpdateOrderItem(OrderItem orderItem);

        Task DeleteOrderItemAsync(int orderItemId);
    }
}
