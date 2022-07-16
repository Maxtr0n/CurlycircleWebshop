using Domain.Entities.Abstractions;
using Domain.Enums;
using System;

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

        public Color Color { get; set; } = Color.Other;

        public Pattern Pattern { get; set; } = Pattern.Other;

        public Material Material { get; set; } = Material.Other;

        public bool IsAvailable { get; set; } = true;

        public Product()
        {
        }

        public void Update(Product updateProduct)
        {
            Name = updateProduct.Name;
            Price = updateProduct.Price;
            Description = updateProduct.Description;
            Color = updateProduct.Color;
            Pattern = updateProduct.Pattern;
            Material = updateProduct.Material;
            IsAvailable = updateProduct.IsAvailable;
        }
    }
}
