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
        public string PaymentId { get; set; } = string.Empty;

        public string PaymentRequestId { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string GatewayUrl { get; set; } = string.Empty;

    }
}
