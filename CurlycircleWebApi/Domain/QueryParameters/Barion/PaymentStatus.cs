using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters.Barion
{
    public enum PaymentStatus
    {
        Prepared = 10,
        Started = 20,
        InProgress = 21,
        Waiting = 22,
        Reserved = 25,
        Authorized = 26,
        Canceled = 30,
        Succeeded = 40,
        Failed = 50,
        PartiallySucceeded = 60,
        Expired = 70
    }
}
