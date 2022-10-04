using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string ThumbnailImageUrl { get; set; } = default!;

        public bool IsAvailable { get; set; } = true;
    }
}
