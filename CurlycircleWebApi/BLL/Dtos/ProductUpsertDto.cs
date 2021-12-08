﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
  public class ProductUpsertDto
  {
    public double Price { get; set; }

    public string Name { get; set; }

    public int ProductCategoryId { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public string Color { get; set; }

    public string Pattern { get; set; }

    public string Material { get; set; }
  }
}
