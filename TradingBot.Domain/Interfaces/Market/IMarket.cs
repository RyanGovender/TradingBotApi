using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Domain.Interfaces.Market
{
    public interface IMarket
    {
        Task<double> GetMarketPrice();
    }
}
