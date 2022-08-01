using Domain.Entities.Abstractions;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Product : EntityBase
    {
        private ProductCategory? _productCategory;
        private int? _productCategoryId;

        public double Price { get; set; }

        public string Name { get; set; } = null!;

        public ProductCategory ProductCategory
        {
            get => _productCategory
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(ProductCategory));
            set => _productCategory = value;
        }

        public int ProductCategoryId
        {
            get => _productCategoryId
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(ProductCategoryId));
            set => _productCategoryId = value;
        }

        public string? Description { get; set; }

        public string ImageUrls { get; set; } = string.Empty;

        public string ThumbnailImageUrl { get; set; } = string.Empty;

        public IEnumerable<Color> Colors { get; set; }

        public Pattern? Pattern { get; set; }

        public Material? Material { get; set; }

        public bool IsAvailable { get; set; } = true;

        public Product()
        {
            Colors = new List<Color>();
        }
    }
}
