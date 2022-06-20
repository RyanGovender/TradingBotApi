using Binance.Net.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Domain.Exchanges.Binance.Common
{
    internal interface IBinanceConnection
    {
        BinanceClient CreateBinanceClient();
    }
}
