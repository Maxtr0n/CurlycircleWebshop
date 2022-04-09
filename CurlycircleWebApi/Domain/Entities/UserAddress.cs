﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserAddress : Address
    {
        private ApplicationUser? _user;

        public ApplicationUser User
        {
            get => _user
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(User));
            set => _user = value;
        }

        public int UserId { get; set; }

        public UserAddress() : base()
        {
        }
    }
}
