﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class OrderItemsViewModel
    {
        public IEnumerable<OrderItemViewModel> OrderItems { get; set; } = default!;
    }
}
