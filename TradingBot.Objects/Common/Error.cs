using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Objects.Common
{
    public class Error
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public object Data { get;set; }
    }
}
