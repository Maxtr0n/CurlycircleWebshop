﻿using Domain.QueryParameters.Barion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class WebPaymentResultViewModel
    {
        public int OrderId { get; set; }

        public string PaymentStatus { get; set; } = string.Empty;

        public int Id { get; set; }
    }
}