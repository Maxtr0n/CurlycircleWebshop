﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class PagedOrdersViewModel : PagedEntityViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; } = default!;
    }
}
