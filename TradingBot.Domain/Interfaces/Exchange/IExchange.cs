using Binance.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Domain.Interfaces.Exchange
{
    public interface IExchange
    {
        Task<BinanceExchangeInfo> GetExchangeData();
    }
}
