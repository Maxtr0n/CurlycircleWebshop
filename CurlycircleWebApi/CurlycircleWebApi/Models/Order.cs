using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Models
{
  public class Order
  {
    public int Id { get; set; }

    public DateTime OrderDateTime { get; set; }

    public string CustomerName { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public List<OrderItem> OrderItems { get; set; }

  }
}
