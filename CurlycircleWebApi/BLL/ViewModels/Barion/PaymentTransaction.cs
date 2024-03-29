﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Barion
{
    public class PaymentTransaction
    {
        public Guid POSTransactionId { get; set; }

        public string Payee { get; set; } = "schutz.mate2@gmail.com";

        public decimal Total { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();
    }
}
