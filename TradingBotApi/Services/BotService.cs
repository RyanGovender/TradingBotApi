using TradingBot.Domain.Factories.TradingFactory;
using TradingBot.Domain.Interfaces.Account;
using TradingBot.Domain.Interfaces.Market;
using TradingBot.Domain.Objects;
using TradingBot.Domain.Services;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;
using TradingBot.Objects.Enum;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Api.Services
{
    public class BotService : IHostedService, IDisposable
    {
        private readonly IMarket _market;
        private readonly IAccount _account;
        private readonly ITradeFactory _tradFactory;
        private readonly ILogger<BotService> _logger;
        private readonly IBotOrder _botOrder;
        private readonly IRepository<Transactions> _transaction;
        public BotService(IBotOrder botOrder, IMarket market, IAccount account, ITradeFactory tradeFactory, ILogger<BotService> logger, IRepository<Transactions> repository)
        {
            _botOrder = botOrder;
            _account = account;
            _market = market;
            _tradFactory = tradeFactory;
            _logger = logger;
            _transaction = repository;
        }

        public void Dispose()
        {
           //add some stuff here later on
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var botOrder = await _botOrder.GetAllAsync();
            var resultbot = botOrder.FirstOrDefault();
            var botAggregate = await _botOrder.GetBotOrderAggregate(resultbot);

            bool runBot = true;
            decimal lastOpPrice = botAggregate.PurchasePrice;
            string tradingSymbol = botAggregate.TradingSymbol;
            bool isFristTrade = botAggregate.IsFirstTrade;

            while (runBot)
            {
                decimal currentPrice = await _market.GetMarketPrice(tradingSymbol);

                if (isFristTrade)
                {
                    lastOpPrice = await _market.PlaceBuyOrder(tradingSymbol);
                    isFristTrade = false;
                }

                var nextOperation = _tradFactory
                    .RunFactory(TradeStrategy.SIMPLE_TRADE, new MarketData { MarketId = tradingSymbol, CurrentPrice = currentPrice, PurchasePrice = lastOpPrice });

                if(nextOperation == botAggregate.NextTradAction)
                {
                    switch (nextOperation)
                    {
                        case Trade.BUY:
                            lastOpPrice = await _market.PlaceBuyOrder(tradingSymbol);
                            await _transaction
                                .InsertAsync(new Transactions { ExchangeID = resultbot.ExchangeID, OpeningBalance = .0m, TransactionAmount = lastOpPrice, TransactionTypeID = 2, UserID = resultbot.UserID });
                            break;
                        case Trade.SELL:
                            lastOpPrice = await _market.PlaceSellOrder(tradingSymbol);
                            await _transaction
                                .InsertAsync(new Transactions { ExchangeID = resultbot.ExchangeID, OpeningBalance = .0m, TransactionAmount = lastOpPrice, TransactionTypeID = 1, UserID = resultbot.UserID });
                            isFristTrade = true;
                            break;
                        case Trade.HOLD:
                            _logger.LogDebug("{0} is holding : current Value {1}", tradingSymbol, currentPrice);
                            continue;
                    }
                }
              
                await Task.Delay(1000, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
