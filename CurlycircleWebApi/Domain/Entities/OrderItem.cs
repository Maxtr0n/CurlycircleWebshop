using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstractions;

namespace Domain.Entities
{
  public class OrderItem : EntityBase
  {
    [Required]
    public Order Order { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public Product Product { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public int Quantity { get; set; }
  }
}
