using Domain.Entities.Abstractions;
using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Product : EntityBase
    {
        public double Price { get; set; }

        public string Name { get; set; }

        private ProductCategory? _productCategory;

        public ProductCategory ProductCategory
        {
            get => _productCategory
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(ProductCategory));
            set => _productCategory = value;
        }

        private int? _productCategoryId;

        public int ProductCategoryId
        {
            get => _productCategoryId
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(ProductCategoryId));
            set => _productCategoryId = value;
        }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public Color Color { get; set; }

        public Pattern Pattern { get; set; }

        public Material Material { get; set; }

        public Product(double price, string name, string? description = null, string? imageUrl = null, Color color = Enums.Color.Other, Pattern pattern = Enums.Pattern.Other, Material material = Enums.Material.Other)
        {
            Price = price;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Color = color;
            Pattern = pattern;
            Material = material;
        }

        public void Update(Product updateProduct)
        {
            Name = updateProduct.Name;
            Price = updateProduct.Price;
            Description = updateProduct.Description;
            Color = updateProduct.Color;
            Pattern = updateProduct.Pattern;
            Material = updateProduct.Material;
        }
    }
}
