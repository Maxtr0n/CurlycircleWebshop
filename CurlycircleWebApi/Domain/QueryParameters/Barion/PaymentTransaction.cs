using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters.Barion
{
    public class PaymentTransaction
    {
        public string POSTransactionId { get; set; } = string.Empty;

        public string Payee { get; set; } = "schutz.mate2@gmail.com";

        public decimal Total { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();
    }
}
