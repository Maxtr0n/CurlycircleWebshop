using Domain.Entities.Abstractions;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string ThumbnailImageUrl { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;

        public List<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void DeleteProducts()
        {
            foreach (var product in Products)
            {
                product.IsAvailable = false;
            }
        }
    }
}
