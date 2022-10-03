using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters.Barion
{
    public class GetPaymentStateResponse : BarionResponse
    {
        public Guid PaymentId { get; set; }

        public string PaymentRequestId { get; set; } = string.Empty;

        public PaymentStatus Status { get; set; }

        public DateTime CompletedAt { get; set; }

        public decimal Total { get; set; }
    }
}
