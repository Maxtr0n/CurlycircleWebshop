using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters.Barion
{
    public class StartPaymentResponse : BarionResponse
    {
        public Guid PaymentId { get; set; }

        public string PaymentRequestId { get; set; } = string.Empty;

        public PaymentStatus Status { get; set; }

        public string GatewayUrl { get; set; } = string.Empty;

        public string CallbackUrl { get; set; } = string.Empty;

        public string RedirectUrl { get; set; } = string.Empty;

    }
}
