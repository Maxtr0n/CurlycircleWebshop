using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IWebPaymentRepository
    {
        int AddWebPayment(WebPayment webPayment);

        Task<IEnumerable<WebPayment>> GetAllAsync();

        Task<WebPayment> GetWebPaymentByIdAsync(int webPaymentId);

        Task<WebPayment> GetWebPaymentByBarionPaymentIdAsync(string barionPaymentId);
    }
}
