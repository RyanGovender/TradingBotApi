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
        public long? BinaceOrderID { get; init; }
        public int TradeStrategyID { get; set; }
        public int OrderTypeID { get; set; }
        public string TradingSymbol { get; set; } = string.Empty;
        public decimal TransactionAmount { get; set; } = decimal.Zero;
        public decimal Quantity { get; set; } = decimal.Zero;
        public bool IsActive { get; set; }
        public Trade NextTradAction { get; set; } = Trade.HOLD;
        public bool? IsOrderFilled { get; set; }
        public bool IsFirstTrade { get; set; } = false;

        public BotOrderAggregate()
        {

        }

        public BotOrderAggregate(Guid botOrderID, int tradeStrategyID, bool isActive, string tradingSymbol, int orderTypeID, decimal quantity)
        {
            BotOrderID = botOrderID;
            TradeStrategyID = tradeStrategyID;
            IsActive = isActive;
            TradingSymbol = tradingSymbol;
            OrderTypeID = orderTypeID;
            Quantity = quantity;
            NextTradAction = Trade.BUY;
            IsFirstTrade = true;
        }
    }
}
