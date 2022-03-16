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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string Address { get; set; }

        public double Total { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string PhoneNumber { get; set; }

        public string? Note { get; set; }

        public Order(string firstName, string lastName, string email, string city, int zipCode, string address, double total, ShippingMethod shippingMethod, PaymentMethod paymentMethod, string phoneNumber, string? note = null)
        {
            OrderDateTime = DateTime.Now;
            OrderItems = new List<OrderItem>();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            City = city;
            ZipCode = zipCode;
            Address = address;
            Total = total;
            ShippingMethod = shippingMethod;
            PaymentMethod = paymentMethod;
            PhoneNumber = phoneNumber;
            Note = note;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }
    }
}