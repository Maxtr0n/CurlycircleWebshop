using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters
{
    public class ProductQueryParameters : QueryParameters
    {
        public int? ProductCategoryId { get; set; }

        public IEnumerable<int> ColorIds { get; set; } = Enumerable.Empty<int>();

        public IEnumerable<int> PatternIds { get; set; } = Enumerable.Empty<int>();

        public IEnumerable<int> MaterialIds { get; set; } = Enumerable.Empty<int>();

        public int MinPrice { get; set; } = 0;

        public int MaxPrice { get; set; } = int.MaxValue;

        public bool ValidPriceRange => MaxPrice >= MinPrice;
    }
}
