using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Objects;
using TradingBot.Domain.Strategies;
using TradingBot.Objects.Enum;

namespace TradingBot.Domain.Factories.TradingFactory
{
    internal class TradeFactory: ITradeFactory
    {
        private static readonly IDictionary<TradeStrategy, ITradeStrategy> _strategyDictionary = new Dictionary<TradeStrategy, ITradeStrategy>();

        public TradeFactory()
        {
            IntializeDictionary();
        }

        public Trade RunFactory(TradeStrategy tradeStrategy, MarketData marketData)
        {
            var getTradeFunction = _strategyDictionary.TryGetValue(tradeStrategy, out var tradeStrategyProcess);

            if (!getTradeFunction || tradeStrategyProcess is null) return Trade.UNKNOWN;

            var result = tradeStrategyProcess.TradeStategy(marketData.CurrentPrice,marketData.PurchasePrice);

            return result;
        }

        private static void IntializeDictionary()
        {
            _strategyDictionary.Add(TradeStrategy.SIMPLE_TRADE, new SimpleTrade());
        } 
    }
}
