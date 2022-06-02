using Domain.Entities.Abstractions;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrls { get; set; }

        public List<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
