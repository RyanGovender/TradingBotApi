using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Enum;
using TradingBot.Domain.Objects;

namespace TradingBot.Domain.Factories.TradingFactory
{
    internal interface ITradeFactory
    {
        Trade RunFactory(TradeStrategy tradeStrategy, MarketData marketData);
    }

    internal interface ITradeStrategy
    {
        TradeStrategy TradeStrategy { get; }
        Trade TradeStategy(double currentPrice, double previousPrice);
    }
}
