using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Color : EntityBase
    {
        public string Name { get; set; } = default!;

        public IEnumerable<Product> Products { get; set; } = default!;
    }
}
