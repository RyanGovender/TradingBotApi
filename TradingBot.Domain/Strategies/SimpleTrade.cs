using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Enum;
using TradingBot.Domain.Factories.TradingFactory;

namespace TradingBot.Domain.Strategies
{
    internal class SimpleTrade : ITradeStrategy
    {
        //Sell thresholds(if in buy state)
        protected const decimal PROFIT_THRESHOLD = 1;
        protected const decimal STOP_LOSS_THRESHOLD = -2;

        //Buy thresholds(if the bot is in Sell state)
        protected const decimal DIP_THRESHOLD = -2;
        protected const decimal UPWARD_TREND_THRESHOLD = 1;

        public TradeStrategy TradeStrategy => TradeStrategy.SIMPLE_TRADE;

        public Trade TradeStategy(decimal currentPrice, decimal previousPrice)
        {
            decimal precentageDiff = (currentPrice - previousPrice) / previousPrice * 100;

            if (precentageDiff >= UPWARD_TREND_THRESHOLD || precentageDiff <= DIP_THRESHOLD)
                return Trade.BUY;
            else if (precentageDiff >= PROFIT_THRESHOLD || precentageDiff <= STOP_LOSS_THRESHOLD)
                return Trade.SELL;
            else
                return Trade.HOLD;
        }
    }
}
