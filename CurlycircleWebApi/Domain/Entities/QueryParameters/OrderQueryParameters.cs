using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.QueryParameters
{
    public class OrderQueryParameters : QueryParameters
    {
        public DateTime MinOrderDate { get; set; } = DateTime.MinValue;

        public DateTime MaxOrderDate { get; set; } = DateTime.Now;

        public bool ValidDateRange => MaxOrderDate > MinOrderDate;

        public int? OrderId { get; set; }

        public string SortDirection { get; set; } = string.Empty;
    }
}
