using Domain.Entities.Abstractions;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Order : EntityBase
    {
        public DateTime OrderDateTime { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public OrderAddress Address { get; set; } = null!;

        public double Total { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Note { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }
    }
}