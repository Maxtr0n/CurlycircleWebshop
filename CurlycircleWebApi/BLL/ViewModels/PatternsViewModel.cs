using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class PatternsViewModel
    {
        public IEnumerable<PatternsViewModel> Patterns { get; set; } = default!;
    }
}
