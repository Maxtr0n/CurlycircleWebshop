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
        public int Id { get; set; }

        public int CartId { get; set; }

        public string Email { get; set; } = null!;

        public Role Role { get; set; }

        public TokenViewModel Token { get; set; } = default!;
    }
}
