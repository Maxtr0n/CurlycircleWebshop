using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        private Cart? _cart;

        public Cart Cart
        {
            get => _cart
                     ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Cart));
            set => _cart = value;
        }

        public List<Order> Orders { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public UserAddress Address { get; set; } = null!;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public ApplicationUser()
        {
            Orders = new List<Order>();
        }
    }
}
