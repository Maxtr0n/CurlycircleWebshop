using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.QueryParameters
{
    public class OrderQueryParameters
    {
        public string Filter { get; set; } = "";

        public SortDirection SortDirection { get; set; } = SortDirection.DESC;

        public int PageIndex { get; set; } = 1;

        const int maxPageSize = 100;
        private int _pageSize = 25;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }

    public enum SortDirection
    {
        DESC,
        ASC
    }
}
