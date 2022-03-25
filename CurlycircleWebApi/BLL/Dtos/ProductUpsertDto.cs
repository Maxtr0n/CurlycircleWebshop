using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class ProductUpsertDto
    {
        public double Price { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int ProductCategoryId { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;

        public string Color { get; set; } = default!;

        public string Pattern { get; set; } = default!;

        public string Material { get; set; } = default!;
    }
}
