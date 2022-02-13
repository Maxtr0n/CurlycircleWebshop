using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace BLL.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public Role Role { get; set; }

        public TokenViewModel Token { get; set; }
    }
}
