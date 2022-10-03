using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters.Barion
{
    public abstract class BarionResponse
    {
        public List<BarionError> Errors { get; set; } = new List<BarionError>();
    }
}
