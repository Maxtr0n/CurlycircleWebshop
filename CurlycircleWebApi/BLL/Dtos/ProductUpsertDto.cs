using Domain.Enums;
using Microsoft.AspNetCore.Http;
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

        public IEnumerable<IFormFile> ProductImages { get; set; } = Enumerable.Empty<IFormFile>();

        public IFormFile? ThumbnailImage { get; set; }

        public Color? Color { get; set; }

        public Pattern? Pattern { get; set; }

        public Material? Material { get; set; }

        public bool? IsAvailable { get; set; }
    }
}
