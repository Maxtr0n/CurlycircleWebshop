﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QueryParameters.Barion
{
    public class BarionError
    {
        public string ErrorCode { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string EndPoint { get; set; } = string.Empty;

        public DateTime HappenedAt { get; set; }
    }
}
