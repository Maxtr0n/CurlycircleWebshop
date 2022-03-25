using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class ErrorViewModel
    {
        public string Message { get; set; } = default!;
        public string? Stacktrace { get; set; } = default!;
    }
}
