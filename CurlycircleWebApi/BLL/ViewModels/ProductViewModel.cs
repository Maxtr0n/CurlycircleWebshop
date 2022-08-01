using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; } = default!;

        public double Price { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int ProductCategoryId { get; set; } = default!;

        public string? Description { get; set; } = default!;

        public IEnumerable<string> ImageUrls { get; set; } = default!;

        public string ThumbnailImageUrl { get; set; } = default!;

        public IEnumerable<Color> Colors { get; set; } = new List<Color>();

        public Pattern? Pattern { get; set; } = default!;

        public Material? Material { get; set; } = default!;

        public bool IsAvailable { get; set; }
    }
}
