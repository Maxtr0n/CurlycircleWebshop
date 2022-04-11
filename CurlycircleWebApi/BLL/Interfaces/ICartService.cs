using BLL.Dtos;
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
        Task<EntityCreatedViewModel> CreateCartAsync();

        Task<CartViewModel> FindCartByIdAsync(int cartId);

        Task DeleteCartAsync(int cartId);

        Task<CartItemsViewModel> GetAllCartItemsAsync(int cartId);

        Task<EntityCreatedViewModel> AddCartItemAsync(int cartId, CartItemUpsertDto cartItemCreateDto);

        Task RemoveCartItemAsync(int cartId, int cartItemId);

        Task ClearCartAsync(int cartId);

        Task UpdateCartItemAsync(int cartId, int cartItemId, int quantity);
    }
}
