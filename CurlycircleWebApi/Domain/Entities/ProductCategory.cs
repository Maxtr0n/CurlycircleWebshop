using Domain.Entities.Abstractions;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<Product> Products { get; set; }

        public ProductCategory(string name, string? description = null)
        {
            Products = new List<Product>();
            Name = name;
            Description = description;
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
