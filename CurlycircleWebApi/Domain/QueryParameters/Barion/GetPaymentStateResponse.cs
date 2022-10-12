using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters.Barion
{
    public class GetPaymentStateResponse : BarionResponse
    {
        public string PaymentId { get; set; } = string.Empty;

        public string PaymentRequestId { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime CompletedAt { get; set; }

        public decimal Total { get; set; }
    }
}
