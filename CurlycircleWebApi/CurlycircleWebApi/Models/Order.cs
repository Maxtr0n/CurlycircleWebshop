using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Models
{
  public class Order
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime OrderDateTime { get; set; }

    public List<OrderItem> OrderItems { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public ShippingMethod ShippingMethod { get; set; }

    [Required]
    public PaymentMethod PaymentMethod { get; set; }

    public string PhoneNumber { get; set; }

    public string Note { get; set; }

  }
}
