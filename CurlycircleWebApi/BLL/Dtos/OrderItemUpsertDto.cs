﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
  public class OrderItemUpsertDto
  {
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }
  }
}
