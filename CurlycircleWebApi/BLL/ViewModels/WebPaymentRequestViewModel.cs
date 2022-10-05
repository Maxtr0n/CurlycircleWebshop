using Domain.QueryParameters.Barion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class WebPaymentRequestViewModel
    {
        public Guid PaymentId { get; set; }

        public string PaymentRequestId { get; set; } = string.Empty;

        public PaymentStatus Status { get; set; }

        public string GatewayUrl { get; set; } = string.Empty;

    }
}
