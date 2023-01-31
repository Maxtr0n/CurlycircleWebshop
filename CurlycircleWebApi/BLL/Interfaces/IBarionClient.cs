using BLL.Dtos.Barion;
using BLL.ViewModels.Barion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBarionClient
    {
        Task<StartPaymentDto> StartPayment(StartPaymentRequestViewModel startPaymentRequest);

        Task<GetPaymentStateDto> GetPaymentState(GetPaymentStateRequestViewModel getPaymentStateRequest);
    }
}
