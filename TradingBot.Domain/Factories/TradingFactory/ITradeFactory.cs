using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Objects;
using TradingBot.Objects.Enum;

namespace TradingBot.Domain.Factories.TradingFactory
{
    public interface ITradeFactory
    {
        Trade RunFactory(TradeStrategy tradeStrategy, MarketData marketData);
    }

    internal interface ITradeStrategy
    {
        TradeStrategy TradeStrategy { get; }
        Trade TradeStategy(decimal currentPrice, decimal previousPrice);
    }
}
