using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class ChangePasswordDto
    {
        public string Email { get; set; } = null!;

        public string Id { get; set; } = null!;

        public string OldPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;
    }
}
