using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class NoAccessException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public NoAccessException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
