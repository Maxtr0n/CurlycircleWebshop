using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
  public interface IOrderRepository
  {
    int AddOrder(Order order);

    Task<IEnumerable<Order>> GetAllAsync();

    Task<Order> GetOrderByIdAsync(int orderId);

    void UpdateOrder(Order order);

    Task DeleteOrderAsync(int orderId);
  }
}
