using Domain.QueryParameters.Barion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBarionClient
    {
        Task<StartPaymentResponse> StartPayment(StartPaymentRequest startPaymentRequest);

        Task<GetPaymentStateResponse> GetPaymentState(GetPaymentStateRequest getPaymentStateRequest);
    }
}
