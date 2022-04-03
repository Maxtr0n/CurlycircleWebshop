using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class CartItemsViewModel
    {
        public IEnumerable<CartItemViewModel> CartItems { get; set; } = default!;
    }
}
