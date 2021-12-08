using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Domain.Entities.Abstractions;

namespace Domain.Entities
{
  public class Product : EntityBase
  {
    [Required]
    public double Price { get; set; }

    [Required]
    public string Name { get; set; }

    public ProductCategory ProductCategory { get; set; }

    public int ProductCategoryId { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public string Color { get; set; }

    public string Pattern { get; set; }

    public string Material { get; set; }

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
