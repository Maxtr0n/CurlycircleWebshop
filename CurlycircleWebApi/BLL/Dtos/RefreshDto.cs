using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class RefreshDto
    {
        public string Email { get; set; } = null!;

        public int Id { get; set; }

        public string AccessToken { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;
    }
}
