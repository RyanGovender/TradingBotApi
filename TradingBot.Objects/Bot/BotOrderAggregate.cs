using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Objects.Enum;

namespace TradingBot.Objects.Bot
{
    public class BotOrderAggregate
    {
        public Guid BotOrderID { get; set; }
        public int TradeStrategyID { get; set; }
        public int OrderTypeID { get; set; }
        public string TradingSymbol { get; set; } = string.Empty;
        public decimal PurchasePrice { get; set; }
        public decimal Quantity { get; set; }
        public bool IsFirstTrade { get; set; }
        public bool IsActive { get; set; }
        public Trade NextTradAction { get; set; } = Trade.HOLD;
    }
}
