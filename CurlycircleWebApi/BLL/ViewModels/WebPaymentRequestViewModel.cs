﻿

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
