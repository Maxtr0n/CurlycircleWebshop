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
        int CreateCartAsync(Cart cart);

        Task<Cart> GetCartByIdAsync(int cartId);

        void UpdateCart(Cart cart);

        Task AddCartItemAsync(int cartId, CartItem cartItem);

        Task DeleteCartAsync(int cartId);
    }
}
