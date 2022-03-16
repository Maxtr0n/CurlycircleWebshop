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
        public string City { get; private set; }

        public string ZipCode { get; private set; }

        public string Line1 { get; private set; }

        public string? Line2 { get; private set; }

        public Address(string line1, string city, string zipCode, string? line2 = null)
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
