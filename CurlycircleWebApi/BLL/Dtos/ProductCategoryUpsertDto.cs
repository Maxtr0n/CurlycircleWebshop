using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class ProductCategoryUpsertDto
    {
        public string Name { get; set; } = default!;

        public string? Description { get; set; } = default!;
    }
}
