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
        int CreateCart(Cart cart);

        Task<Cart> GetCartByIdAsync(int cartId);

        Task<Cart> GetUserCartAsync(int userId);

        void UpdateCart(Cart cart);

        Task AddCartItemAsync(int cartId, CartItem cartItem);

        Task DeleteCartAsync(int cartId);
    }
}
