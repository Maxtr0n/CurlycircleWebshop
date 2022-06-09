using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class ProductUpsertDto
    {
        public double Price { get; set; }

        public string Name { get; set; } = null!;

        public int ProductCategoryId { get; set; }

        public string? Description { get; set; }

        public List<string> ImageUrls { get; set; } = default!;

        public string? Color { get; set; }

        public string? Pattern { get; set; }

        public string? Material { get; set; }

        public bool? IsAvailable { get; set; }
    }
}
