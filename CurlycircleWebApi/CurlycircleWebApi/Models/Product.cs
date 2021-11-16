using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Models
{
  public class Product
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public string Name { get; set; }

    public ProductCategory ProductCategory { get; set; }

    public int ProductCategoryId { get; set; }

    public string Description { get; set; }

    public string Color { get; set; }

    public string Pattern { get; set; }

    public string Material { get; set; }

  }
}
