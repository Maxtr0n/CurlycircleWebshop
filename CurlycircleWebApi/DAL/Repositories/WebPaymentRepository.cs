using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WebPaymentRepository : IWebPaymentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WebPaymentRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public int AddWebPayment(WebPayment webPayment)
        {
            _dbContext.WebPayments.Add(webPayment);
            return webPayment.Id;
        }

        public async Task<IEnumerable<WebPayment>> GetAllAsync()
        {
            var webPayments = await _dbContext.WebPayments
               .ToListAsync();
            return webPayments;
        }

        public async Task<WebPayment> GetWebPaymentByIdAsync(int webPaymentId)
        {
            var webPayment = await _dbContext.WebPayments
               .FirstOrDefaultAsync(wp => wp.Id == webPaymentId);

            if (webPayment == null)
            {
                throw new EntityNotFoundException($"Web payment with id {webPaymentId} not found.");
            }

            return webPayment;
        }
    }
}
