using Domain.Entities.Abstractions;
using System;

namespace Domain.Entities
{
    public class OrderItem : EntityBase
    {
        private Order? _order;
        private int? _orderId;
        private Product? _product;
        private int? _productId;

        public Order Order
        {
            get => _order
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Order));
            set => _order = value;
        }

        public int OrderId
        {
            get => _orderId
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(OrderId));
            set => _orderId = value;
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

        public OrderItem(double price, int quantity)
        {
            Price = price;
            Quantity = quantity;
        }
    }
}
