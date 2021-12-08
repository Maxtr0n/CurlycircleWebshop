using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
  public class ProductCategoriesViewModel
  {
    public IEnumerable<ProductCategoryViewModel> ProductCategories { get; set; }
  }
}
