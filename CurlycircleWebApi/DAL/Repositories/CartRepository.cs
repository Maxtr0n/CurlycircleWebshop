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
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CartRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int CreateCartAsync(Cart cart)
        {
            dbContext.Carts.Add(cart);
            return cart.Id;
        }

        public async Task DeleteCartAsync(int cartId)
        {
            var cart = await dbContext.Carts.FindAsync(cartId);
            if (cart != null)
            {
                dbContext.Carts.Remove(cart);
            }
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

        public async Task AddCartItemAsync(int cartId, CartItem cartItem)
        {
            var cart = await dbContext.Carts.FindAsync(cartId);

            if (cart == null)
            {
                throw new EntityNotFoundException($"Cart with id {cartId} not found.");
            }

            cart.AddCartItem(cartItem);
        }

        public void UpdateCart(Cart cart)
        {
            dbContext.Carts.Update(cart);
        }

        public async Task<Cart> GetUserCartAsync(int userId)
        {
            var cart = await dbContext.Carts.Where(c => c.ApplicationUserId == userId).FirstOrDefaultAsync();

            if (cart == null)
            {
                throw new EntityNotFoundException($"Could not find cart for user with Id {userId}.");
            }

            return cart;
        }
    }
}
