using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address : ValueObject
    {
        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Line1 { get; set; }

        public string? Line2 { get; set; }

        public Address(string city, string zipCode, string line1, string? line2)
        {
            City = city;
            ZipCode = zipCode;
            Line1 = line1;
            Line2 = line2;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return ZipCode;
            yield return Line1;
            yield return Line2 ?? "";
        }
    }
}
