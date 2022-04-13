using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserAddress : Address
    {
        private ApplicationUser? _applicationUser;

        public ApplicationUser ApplicationUser
        {
            get => _applicationUser
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(ApplicationUser));
            set => _applicationUser = value;
        }

        public int ApplicationUserId { get; set; }

        public UserAddress() : base()
        {
        }
    }
}
