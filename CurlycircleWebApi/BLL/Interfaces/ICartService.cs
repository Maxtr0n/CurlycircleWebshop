using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICartService
    {
        Task<EntityCreatedViewModel> CreateCartForAnonymousUserAsync();

        Task<OrderItemsViewModel> GetAllOrderItemsAsync(int cartId);

        Task DeleteCartAsync(int cartId);

        Task<EntityCreatedViewModel> AddOrderItemAsync(int orderItemId);


    }
}
