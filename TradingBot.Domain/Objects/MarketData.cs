using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Objects.Enums;

namespace TradingBot.Domain.Objects
{
    public class MarketData
    {
        public string MarketId { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PreviousPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public Trade NextAction { get; set; } = Trade.HOLD;
    }
}
