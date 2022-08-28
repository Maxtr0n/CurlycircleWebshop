using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Color : EntityBase
    {
        public string Name { get; set; } = default!;

        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; } = default!;

        public Color()
        {
            Products = new List<Product>();
        }
    }
}
