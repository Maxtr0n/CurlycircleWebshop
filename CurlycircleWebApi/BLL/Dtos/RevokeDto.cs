using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class RevokeDto
    {
        public string Email { get; set; } = default!;
        public int Id { get; set; }
    }
}
