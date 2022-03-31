using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; } = default!;

        public string NewPassword { get; set; } = default!;
    }
}
