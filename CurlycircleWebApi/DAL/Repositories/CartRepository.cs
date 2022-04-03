using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CartRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int AddOrderItem(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderItem(int orderItemId)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            var cart = await dbContext.Carts.FindAsync(cartId);

            if (cart == null)
            {
                throw new EntityNotFoundException($"Cart with id {cartId} not found.");
            }

            return cart;
        }

        public async Task<IEnumerable<OrderItem>> GetCartOrderItemsAsync(int cartId)
        {
            throw new NotImplementedException();

        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }
    }
}
