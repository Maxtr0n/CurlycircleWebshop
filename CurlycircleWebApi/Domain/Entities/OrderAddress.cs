﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderAddress : Address
    {
        private Order? _order;

        public Order Order
        {
            get => _order
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Order));
            set => _order = value;
        }

        public int OrderId { get; set; }

        public OrderAddress(string city, string zipCode, string line1, string? line2) : base(city, zipCode, line1, line2)
        {
        }
    }
}
