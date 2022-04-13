using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class CartItemViewModel
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public ProductViewModel Product { get; set; } = null!;

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
