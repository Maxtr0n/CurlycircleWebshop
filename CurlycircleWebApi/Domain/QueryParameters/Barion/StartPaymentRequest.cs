using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters.Barion
{
    public class StartPaymentRequest
    {
        public string POSKey { get; set; } = string.Empty;

        public string PaymentType { get; set; } = "Immediate";

        public bool GuestCheckout { get; set; } = true;

        public List<string> FundingSources { get; set; } = new List<string> { "Bank card", "Barion balance", "Apple Pay", "Google Pay" };

        // Nekem kell generálni, minden payment request-hez saját identifier, egy orderhez 0 vagy 1 tartozik (ha webes fizetés akkor 1)
        public string PaymentRequestId { get; set; } = string.Empty;

        // The URL where the payer should be redirected after the payment is completed or canceled.
        // The payment identifier is added to the query string part of this URL in the paymentId parameter.
        // If not provided, the system will use the redirect URL assigned to the shop that started the payment.
        public string RedirectUrl { get; set; } = string.Empty;

        // The URL where the Barion system sends a request whenever there is a change in the state of the payment.
        // The payment identifier is added to the query string part of this URL in the paymentId parameter.
        public string CallbackUrl { get; set; } = string.Empty;

        // An array of payment transactions contained in the payment. A payment must contain at least one such transaction.
        // Nálam csak egy Transaction lesz itt mindig, ami a vevő fizetését reprezentálja.
        public List<PaymentTransaction> Transactions { get; set; } = new List<PaymentTransaction>();

        // Nálam ez a payment-hez tartozó Order Id-ja
        public string? OrderNumber { get; set; }

        public string Locale { get; set; } = "hu-HU";

        public string Currency { get; set; } = "HUF";
    }
}
