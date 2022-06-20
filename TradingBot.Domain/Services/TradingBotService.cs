using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Factories.TradingFactory;
using TradingBot.Domain.Interfaces.Account;
using TradingBot.Domain.Interfaces.Market;

namespace TradingBot.Domain.Services
{
    internal class TradingBotService
    {
        private readonly IMarket _market;
        private readonly IAccount _account;
        private readonly ITradeFactory _tradFactory;

        public TradingBotService(IMarket market, IAccount account, ITradeFactory tradeFactory)
        {
            _account = account;
            _market = market;
            _tradFactory = tradeFactory;
        }

        public async Task RunBot()
        {
            bool isNextOperationBuy = true;
            bool runBot = true;
            decimal lastOpPrice = 0;


            while (runBot)
            {
                decimal currentPrice = await _market.GetMarketPrice("BTCUSDT");

                var nextOperation = _tradFactory.RunFactory(Enum.TradeStrategy.SIMPLE_TRADE, new Objects.MarketData { CurrentPrice = currentPrice, PreviousPrice = lastOpPrice });

                if (nextOperation == Enum.Trade.SELL)
                    lastOpPrice = await _market.PlaceSellOrder("BTCUSDT");
                else
                    lastOpPrice = await _market.PlaceBuyOrder("BTCUSDT");

                await Task.Delay(1000);
            }
        }
    }
}
