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
        public int CartId { get; set; }

        public int? ApplicationUserId { get; set; }

        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string City { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public string Line1 { get; set; } = null!;

        public string? Line2 { get; set; }

        public ShippingMethod ShippingMethod { get; set; } = default!;

        public PaymentMethod PaymentMethod { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public string Note { get; set; } = default!;
    }
}
