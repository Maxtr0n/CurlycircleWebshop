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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public ApplicationUser(string email, string firstName, string lastName, Address address)
        {
            Orders = new List<Order>();
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }
    }
}
