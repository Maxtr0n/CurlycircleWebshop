using Domain.Entities.Abstractions;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cart : EntityBase
    {

        public List<OrderItem> OrderItems { get; set; }

        public Cart()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
