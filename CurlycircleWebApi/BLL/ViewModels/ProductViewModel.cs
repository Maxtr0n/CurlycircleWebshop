using System.Collections.Generic;

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

        public IEnumerable<ColorViewModel> Colors { get; set; } = new List<ColorViewModel>();

        public PatternViewModel? Pattern { get; set; } = default!;

        public MaterialViewModel? Material { get; set; } = default!;

        public bool IsAvailable { get; set; }
    }
}
