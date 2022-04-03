using BLL.Interfaces;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CartService : ICartService
    {
        public Task<EntityCreatedViewModel> AddOrderItemAsync(int orderItemId)
        {
            throw new NotImplementedException();
        }

        public Task<EntityCreatedViewModel> CreateCartForAnonymousUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteCartAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItemsViewModel> GetAllOrderItemsAsync(int cartId)
        {
            throw new NotImplementedException();
        }
    }
}
