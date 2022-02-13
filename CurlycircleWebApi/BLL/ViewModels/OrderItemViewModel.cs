using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace BLL.ViewModels
{
  public class OrderItemViewModel
  {
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }
  }
}
