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

        public string Name { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string Address { get; set; }

        public double Total { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string PhoneNumber { get; set; }

        public string? Note { get; set; }

        public Order()
        {
            OrderDateTime = DateTime.Now;
            OrderItems = new List<OrderItem>();
            Name = string.Empty;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }
    }
}