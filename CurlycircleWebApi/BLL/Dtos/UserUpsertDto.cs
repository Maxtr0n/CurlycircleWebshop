using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class UserUpsertDto
    {
        public string Email { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string City { get; private set; } = default!;

        public string ZipCode { get; private set; } = default!;

        public string Line1 { get; private set; } = default!;

        public string? Line2 { get; private set; }

        public string PhoneNumber { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
