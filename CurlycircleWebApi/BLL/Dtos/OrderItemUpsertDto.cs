using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class OrderItemUpsertDto
    {
        public int OrderId { get; set; } = default!;

        public int ProductId { get; set; } = default!;

        public double Price { get; set; } = default!;

        public int Quantity { get; set; } = default!;
    }
}
