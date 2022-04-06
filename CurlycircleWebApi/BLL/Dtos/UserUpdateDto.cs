using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class UserUpdateDto
    {
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string City { get; set; } = null!;

        public string ZipCode { get; set; } = null!;

        public string Line1 { get; set; } = null!;

        public string? Line2 { get; set; }

        public string PhoneNumber { get; set; } = null!;
    }
}
