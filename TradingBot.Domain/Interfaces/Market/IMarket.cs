using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Domain.Interfaces.Market
{
    public interface IMarket
    {
        Task<decimal> GetMarketPrice(string currencySymbol);
        Task<decimal> PlaceSellOrder(string currencySymbol);
        Task<decimal> PlaceBuyOrder(string currencySymbol);
    }
}
