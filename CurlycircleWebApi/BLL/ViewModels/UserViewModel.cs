using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = default!;

        public string Email { get; set; } = default!;

        public Role Role { get; set; } = default!;

        public TokenViewModel Token { get; set; } = default!;
    }
}
