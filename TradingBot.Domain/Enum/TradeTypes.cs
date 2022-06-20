using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Domain.Enum
{
  public enum Trade
    {
        SELL = 0,
        BUY = 1,
        HOLD = 2,
        UNKNOWN = 3
    }

    public enum TradeStrategy
    {
        SIMPLE_TRADE = 1
    }
}
