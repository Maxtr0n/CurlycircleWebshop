using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class RefreshDto
    {
        public string Email { get; set; } = default!;

        public string AccessToken { get; set; } = default!;

        public string RefreshToken { get; set; } = default!;
    }
}
