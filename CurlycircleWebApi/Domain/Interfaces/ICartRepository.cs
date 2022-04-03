using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByIdAsync(int cartId);

        Task<IEnumerable<OrderItem>> GetCartOrderItemsAsync(int cartId);

        int AddOrderItem(OrderItem orderItem);

        void UpdateOrderItem(OrderItem orderItem);

        Task DeleteOrderItem(int orderItemId);
    }
}
