using Binance.Net.Clients;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Domain.Exchanges.Binance.Common
{
    internal class BinanceConnection : IBinanceConnection
    {
        public BinanceClient CreateBinanceClient()
        {
            var binanceClient = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET"),
                // Set options here for this client
            });

            return binanceClient;
        }
    }
}
