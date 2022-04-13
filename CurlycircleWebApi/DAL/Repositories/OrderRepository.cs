using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int AddOrder(Order order)
        {
            dbContext.Orders.Add(order);
            return order.Id;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new EntityNotFoundException($"Order with id {orderId} not found.");
            }

            return order;
        }

        public void UpdateOrder(Order order)
        {
            dbContext.Orders.Update(order);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if (order != null)
            {
                dbContext.Orders.Remove(order);
            }
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.ApplicationUserId == userId)
                .ToListAsync();
            return orders;
        }
    }
}
