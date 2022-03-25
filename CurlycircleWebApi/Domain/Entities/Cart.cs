using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cart : EntityBase
    {
        private ApplicationUser? _applicationUser;

        public ApplicationUser ApplicationUser
        {
            get => _applicationUser
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(ApplicationUser));
            set => _applicationUser = value;
        }

        public List<OrderItem> OrderItems { get; set; }

        public Cart()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
