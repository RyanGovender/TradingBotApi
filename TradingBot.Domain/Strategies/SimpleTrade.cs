using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Factories.TradingFactory;
using TradingBot.Domain.Objects;
using TradingBot.Objects.Enums;

namespace TradingBot.Domain.Strategies
{
    internal class SimpleTrade : ITradeStrategy
    {
        //Sell thresholds(if in buy state)
        protected const decimal PROFIT_THRESHOLD = .5m;
        protected const decimal STOP_LOSS_THRESHOLD = -2.25m;

        //Buy thresholds(if the bot is in Sell state)
        protected const decimal DIP_THRESHOLD = -2.25m;
        protected const decimal UPWARD_TREND_THRESHOLD = 1.25m;

        public TradeStrategy TradeStrategy => TradeStrategy.SIMPLE_TRADE;

        public Trade TradeStategy(MarketData marketData!!)
        {
            if (marketData.PurchasePrice <= 0)
                return Trade.BUY;

            decimal precentageDiff = (marketData.CurrentPrice - marketData.PurchasePrice) / marketData.PurchasePrice * 100;

            if (marketData.NextAction is Trade.BUY && (precentageDiff >= UPWARD_TREND_THRESHOLD || precentageDiff <= DIP_THRESHOLD))
                return Trade.BUY;

            if (marketData.NextAction is Trade.SELL && (precentageDiff >= PROFIT_THRESHOLD || precentageDiff <= STOP_LOSS_THRESHOLD))
                return Trade.SELL;
          
                return Trade.HOLD;
        }
    }
}
