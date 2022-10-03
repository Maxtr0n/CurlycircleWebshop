using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters.Barion
{
    public class GetPaymentStateRequest
    {
        public Guid POSKey { get; set; }

        public Guid PaymentId { get; set; }
    }
}
