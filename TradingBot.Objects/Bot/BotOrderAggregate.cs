using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Objects.Enums;

namespace TradingBot.Objects.Bot
{
    public class BotOrderAggregate
    {
        public Guid BotOrderID { get; set; }
        public int TradeStrategyID { get; set; }
        public int OrderTypeID { get; set; }
        public string TradingSymbol { get; set; } = string.Empty;
        public decimal PurchasePrice { get; set; } = decimal.Zero;
        public decimal Quantity { get; set; } = decimal.Zero;
        public bool IsActive { get; set; }
        public Trade NextTradAction { get; set; } = Trade.HOLD;
        public bool IsFirstTrade { get; set; } = false;
    }
}
