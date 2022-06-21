using Binance.Net.Clients;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
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
                ApiCredentials = new ApiCredentials("WqXgTPBUpALIVM3ULGGHhuLXNEsogJIhsnjmkpTuM2zyPJpwVRfqOaa6VZg654Vf", "oNB7xetILJl9iQxsi0hFMQCkNZVT98h0titLAdJXtmNwFWKdffP0M9rcTL7XPFxC"),
                // Set options here for this client

                SpotApiOptions = new BinanceApiClientOptions
                {
                    BaseAddress = "https://testnet.binance.vision",
                    RateLimitingBehaviour = RateLimitingBehaviour.Fail
                },
            });

            return binanceClient;
        }
    }
}
