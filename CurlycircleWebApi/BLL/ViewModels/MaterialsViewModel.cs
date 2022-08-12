using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class MaterialsViewModel
    {
        public IEnumerable<MaterialViewModel> Materials { get; set; } = default!;
    }
}
