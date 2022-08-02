using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Material : EntityBase
    {
        public string Name { get; set; } = null!;

        public IEnumerable<Product> Products { get; set; } = default!;

        public Material()
        {
            Products = new List<Product>();
        }
    }
}
