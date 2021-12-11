using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstractions;
using Domain.Enums;

namespace Domain.Entities
{
    public class Order : EntityBase
    {
        [Required] public DateTime OrderDateTime { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string City { get; set; }

        [Required] public int ZipCode { get; set; }

        [Required] public string Address { get; set; }

        [Required] public double Total { get; set; }

        [Required] public ShippingMethod ShippingMethod { get; set; }

        [Required] public PaymentMethod PaymentMethod { get; set; }

        public string PhoneNumber { get; set; }

        public string Note { get; set; }

        public void AddOrderItem(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }
    }
}