using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public IEnumerable<CartItemViewModel> CartItems { get; set; } = default!;
    }
}
