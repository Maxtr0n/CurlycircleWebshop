using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters.Barion
{
    public class StartPaymentRequest
    {
        public Guid POSKey { get; set; }

        public string PaymentType { get; set; } = "Immediate";

        public bool GuestCheckout { get; set; } = true;

        public List<string> FundingSources { get; set; } = new List<string>();

        public string PaymentRequestId { get; set; } = string.Empty;

        public string RedirectUrl { get; set; } = string.Empty;

        public string CallbackUrl { get; set; } = string.Empty;

        public List<PaymentTransaction> Transactions { get; set; } = new List<PaymentTransaction>();

        public string? OrderNumber { get; set; }

        public string Locale { get; set; } = "hu-HU";

        public string Currency { get; set; } = "HUF";
    }
}
