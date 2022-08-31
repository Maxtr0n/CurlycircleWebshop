using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class PagedProductsViewModel : PagedEntityViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; } = default!;
    }
}
