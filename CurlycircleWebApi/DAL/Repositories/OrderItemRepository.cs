using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderItemRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int AddOrderItemItem(OrderItem orderItem)
        {
            dbContext.OrderItems.Add(orderItem);
            return orderItem.Id;
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int orderItemId)
        {
            var orderItem = await dbContext.OrderItems
                .FirstOrDefaultAsync(o => o.Id == orderItemId);

            if (orderItem == null)
            {
                throw new EntityNotFoundException($"OrderItem with id {orderItemId} not found.");
            }

            return orderItem;
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            dbContext.OrderItems.Update(orderItem);
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            var orderItem = await dbContext.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                dbContext.OrderItems.Remove(orderItem);
            }
        }
    }
}
