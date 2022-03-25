using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; } = default!;

        public int OrderId { get; set; } = default!;

        public int ProductId { get; set; } = default!;

        public Product Product { get; set; } = default!;

        public double Price { get; set; } = default!;

        public int Quantity { get; set; } = default!;
    }
}
