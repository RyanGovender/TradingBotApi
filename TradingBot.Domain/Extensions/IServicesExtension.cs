using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Exchanges.Binance.Account;
using TradingBot.Domain.Exchanges.Binance.Common;
using TradingBot.Domain.Exchanges.Binance.Exchange;
using TradingBot.Domain.Exchanges.Binance.Market;
using TradingBot.Domain.Factories.TradingFactory;
using TradingBot.Domain.Interfaces.Account;
using TradingBot.Domain.Interfaces.Exchange;
using TradingBot.Domain.Interfaces.Market;
using TradingBot.Domain.Services;
using TradingBot.Domain.Strategies;

namespace TradingBot.Domain.Extensions
{
    public static class IServicesExtension
    {
        public static void RunBotTrader(this IServiceCollection services)
        {
            services.AddSingleton<IBinanceConnection, BinanceConnection>();
            services.AddSingleton<ITradingBotService, TradingBotService>();
            services.AddSingleton<IAccount, Account>();
            services.AddSingleton<IMarket, Market>();
            services.AddSingleton<IExchange, ExchangeBinance>();
            services.AddSingleton<ITradeFactory, TradeFactory>();
           // services.AddSingleton<ITradeStrategy, SimpleTrade>();
        }
    }
}
