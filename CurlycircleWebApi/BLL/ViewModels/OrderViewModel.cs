using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; } = default!;

        public DateTime OrderDateTime { get; set; } = default!;

        public IEnumerable<OrderItemViewModel> OrderItems { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public Address Address { get; set; } = default!;

        public double Total { get; set; } = default!;

        public ShippingMethod ShippingMethod { get; set; } = default!;

        public PaymentMethod PaymentMethod { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public string? Note { get; set; } = default!;
    }
}
