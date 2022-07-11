using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Factories.TradingFactory;
using TradingBot.Domain.Interfaces.Account;
using TradingBot.Domain.Interfaces.Market;
using TradingBot.Objects.Enums;

namespace TradingBot.Domain.Services
{
    public class TradingBotService: ITradingBotService
    {
        private readonly IMarket _market;
        private readonly IAccount _account;
        private readonly ITradeFactory _tradFactory;
        private readonly ILogger<TradingBotService> _logger;

        public TradingBotService(IMarket market, IAccount account, ITradeFactory tradeFactory, ILogger<TradingBotService> logger)
        {
            _account = account;
            _market = market;
            _tradFactory = tradeFactory;
            _logger = logger;
        }

        public async Task RunBot()
        {
            bool runBot = true;
            decimal lastOpPrice = 0;
            string tradingSymbol = "ETHBTC";
            bool isFristTrade = true;

            while (runBot)
            {
                //decimal currentPrice = await _market.GetMarketPrice(tradingSymbol);

                //if (isFristTrade)
                //{
                //    lastOpPrice = await _market.PlaceBuyOrder(tradingSymbol);
                //    isFristTrade=false;
                //}

                //var nextOperation = _tradFactory
                //    .RunFactory(TradeStrategy.SIMPLE_TRADE, new Objects.MarketData { MarketId = tradingSymbol, CurrentPrice = currentPrice, PurchasePrice = lastOpPrice });

                //switch (nextOperation)
                //{
                //    case Trade.BUY:
                //        lastOpPrice = await _market.PlaceBuyOrder(tradingSymbol);
                //        break;
                //    case Trade.SELL:
                //        lastOpPrice = await _market.PlaceSellOrder(tradingSymbol);
                //        isFristTrade = true;
                //        break;
                //    case Trade.HOLD:
                //        _logger.LogDebug("{0} is holding : current Value {1}",tradingSymbol, currentPrice);
                //        continue;
                //}

                await Task.Delay(1000);
            }
        }
    }
}
