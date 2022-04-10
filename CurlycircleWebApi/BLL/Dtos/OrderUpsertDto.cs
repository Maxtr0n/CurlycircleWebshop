using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class OrderUpsertDto
    {
        public DateTime OrderDateTime { get; set; } = default!;

        public List<OrderItemUpsertDto> OrderItems { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string City { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public string Line1 { get; set; } = null!;

        public string? Line2 { get; set; }

        public double Total { get; set; } = default!;

        public ShippingMethod ShippingMethod { get; set; } = default!;

        public PaymentMethod PaymentMethod { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public string Note { get; set; } = default!;
    }
}
