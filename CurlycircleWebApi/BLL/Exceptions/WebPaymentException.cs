using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class WebPaymentException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public WebPaymentException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
