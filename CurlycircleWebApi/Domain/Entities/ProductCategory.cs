using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstractions;

namespace Domain.Entities
{
  public class ProductCategory : EntityBase
  {
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public List<Product> Products { get; set; }

    public void AddProduct(Product product)
    {
      Products.Add(product);
    }
  }
}
