using Binance.Net.Clients;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Extensions;

namespace TradingBot.Domain.Exchanges.Binance.Common
{
    internal class BinanceConnection : IBinanceConnection
    {
        private readonly IConfiguration _config;
        public BinanceConnection(IConfiguration configuration)
        {
            _config = configuration;
        }

        public BinanceClient CreateBinanceClient()
        {
            var binanceClient = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(_config.GetBinanceKey("API-KEY"),_config.GetBinanceKey("SECRET-KEY")),
                // Set options here for this client

                SpotApiOptions = new BinanceApiClientOptions
                {
                    BaseAddress = "https://testnet.binance.vision",
                    RateLimitingBehaviour = RateLimitingBehaviour.Fail
                },
            }); ;

            return binanceClient;
        }
    }
}
