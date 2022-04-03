using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CartItem : EntityBase
    {
        private Cart? _cart;
        private int? _cartId;
        private Product? _product;
        private int? _productId;

        public Cart Cart
        {
            get => _cart
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Cart));
            set => _cart = value;
        }

        public int CartId
        {
            get => _cartId
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(CartId));
            set => _cartId = value;
        }

        public Product Product
        {
            get => _product
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Product));
            set => _product = value;
        }

        public int ProductId
        {
            get => _productId
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(ProductId));
            set => _productId = value;
        }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
