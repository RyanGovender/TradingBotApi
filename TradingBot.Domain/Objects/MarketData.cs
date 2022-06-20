using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Domain.Objects
{
    internal class MarketData
    {
        public string MarketId { get; set; }
        public double CurrentPrice { get; set; }
        public double PreviousPrice { get; set; }
        public double PurchasePrice { get; set; }
    }
}
