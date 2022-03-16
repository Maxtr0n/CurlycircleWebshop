using Domain.Entities.Abstractions;
using System;

namespace Domain.Entities
{
    public class OrderItem : EntityBase
    {
        private Order? _order;

        private Product? _product;

        public Order Order
        {
            get => _order
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Order));
            set => _order = value;
        }

        public Product Product
        {
            get => _product
                ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Product));
            set => _product = value;
        }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public OrderItem(double price, int quantity)
        {
            Price = price;
            Quantity = quantity;
        }
    }
}
