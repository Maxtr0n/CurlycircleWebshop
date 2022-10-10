using Domain.Entities.Abstractions;
using Domain.QueryParameters.Barion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WebPayment : EntityBase
    {
        private Order? _order;
        private int? _orderId;

        public Order Order
        {
            get => _order
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Order));
            set => _order = value;
        }

        public int OrderId
        {
            get => _orderId
                    ?? throw new InvalidOperationException("Uninitialized property: " + nameof(OrderId));
            set => _orderId = value;
        }

        public Guid POSTransactionId { get; set; } = Guid.NewGuid();

        public string? BarionPaymentId { get; set; }

        public double Total { get; set; }

        public string PaymentStatus { get; set; } = default!;

        public WebPayment()
        {
        }
    }
}
