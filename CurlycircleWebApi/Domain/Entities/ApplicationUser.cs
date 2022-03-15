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
        public Cart Cart { get; set; }

        public List<Order> Orders { get; set; }

        public ApplicationUser()
        {
            this.Cart = new Cart();
            this.Orders = new List<Order>();
        }
    }
}
