using Domain.Entities.Abstractions;
using Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cart : EntityBase
    {
        public ApplicationUser? ApplicationUser { get; set; }

        public int? ApplicationUserId { get; set; }

        public List<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public void AddCartItem(CartItem newCartItem)
        {
            foreach (var item in CartItems)
            {
                if (item.ProductId == newCartItem.ProductId)
                {
                    item.Quantity += newCartItem.Quantity;
                    return;
                }
            }
            CartItems.Add(newCartItem);
        }

        public void RemoveCartItem(int cartItemId)
        {
            var cartItem = CartItems.Find(c => c.Id == cartItemId);
            if (cartItem != null)
            {
                CartItems.Remove(cartItem);
            }
        }

        public void UpdateQuantity(int cartItemId, int quantity)
        {
            CartItem? cartItem = CartItems.Find(c => c.Id == cartItemId);

            if (cartItem == null)
            {
                throw new EntityNotFoundException("Cart item not found!");
            }

            if (quantity == 0)
            {
                CartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
            }
        }

        public void ClearCart()
        {
            CartItems.Clear();
        }
    }
}
