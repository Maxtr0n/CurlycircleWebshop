using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.QueryParameters;
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

        public async Task<PagedList<Order>> GetAllAsync(OrderQueryParameters orderQueryParameters)
        {
            if (!orderQueryParameters.ValidDateRange)
            {
                throw new BadParameterException("Max date of order cannot be less than min date of order.");
            }

            var orders = dbContext.Orders
                .Where(o => o.OrderDateTime.Date >= orderQueryParameters.MinOrderDate.Date && o.OrderDateTime.Date <= orderQueryParameters.MaxOrderDate.Date);

            SearchById(ref orders, orderQueryParameters.OrderId);

            ApplySort(ref orders, orderQueryParameters.SortDirection);

            return await PagedList<Order>.CreateAsync(orders
                .Include(o => o.OrderItems), orderQueryParameters.PageIndex, orderQueryParameters.PageSize);
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

        private void SearchById(ref IQueryable<Order> orders, int? orderId)
        {
            if (!orders.Any() || orderId == null)
                return;
            orders = orders.Where(o => o.Id == orderId);
        }

        private void ApplySort(ref IQueryable<Order> orders, string sortDirection)
        {
            if (sortDirection.Equals("asc"))
            {
                orders = orders.OrderBy(o => o.OrderDateTime).ThenBy(o => o.Id);
            }
            else
            {
                orders = orders.OrderByDescending(o => o.OrderDateTime).ThenBy(o => o.Id);
            }
        }
    }
}
