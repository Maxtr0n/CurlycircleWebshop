using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Barion
{
    public class Item
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public string Unit { get; set; } = "darab";

        public decimal UnitPrice { get; set; }

        public decimal ItemTotal { get; set; }
    }
}
