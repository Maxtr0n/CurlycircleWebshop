using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Barion
{
    public class GetPaymentStateRequestViewModel
    {
        public string POSKey { get; set; } = default!;

        public string PaymentId { get; set; } = default!;
    }
}
