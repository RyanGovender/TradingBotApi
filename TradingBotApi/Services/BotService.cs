using TradingBot.Domain.Factories.TradingFactory;
using TradingBot.Domain.Interfaces.Account;
using TradingBot.Domain.Interfaces.Market;
using TradingBot.Domain.Objects;
using TradingBot.Domain.Services;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;
using TradingBot.Objects.Enums;
using TradingBot.Objects.Order;
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
            var resultbot = botOrder.FirstOrDefault(X=>X.ID == Guid.Parse("03fc7c7a-f600-4e12-9f77-659730c70dd4"));
            bool runBot = true;

            while (runBot)
            {
                var botAggregate = await _botOrder.GetBotOrderAggregate(resultbot);
               // decimal lastOpPrice = botAggregate.PurchasePrice;
               // string tradingSymbol = botAggregate.TradingSymbol;
               //bool isFristTrade = botAggregate.IsFirstTrade;

                decimal currentPrice = await _market.GetMarketPrice(botAggregate.TradingSymbol);
                

                if (botAggregate.IsFirstTrade)
                {
                    var buyPrice = await _market.PlaceOrder(new PlaceOrderData
                    { CurrencySymbol = botAggregate.TradingSymbol, OrderSideId = (int)Trade.BUY, OrderTypeId = botAggregate.OrderTypeID, Quantity = botAggregate.Quantity, Price = currentPrice }); //await _market.PlaceBuyOrder(botAggregate.TradingSymbol);
                    await _transaction
                        .InsertAsync(new Transactions(TransactionType.BUY, buyPrice.Price, resultbot.UserID, resultbot.ExchangeID, buyPrice.QuantityFilled));
                }

                var nextOperation = _tradFactory
                    .RunFactory(TradeStrategy.SIMPLE_TRADE, new MarketData { MarketId = botAggregate.TradingSymbol, CurrentPrice = currentPrice, PurchasePrice = botAggregate.PurchasePrice, NextAction = botAggregate.NextTradAction });

                switch (nextOperation)
                    {
                        case Trade.BUY:
                        var buyPrice = await _market.PlaceOrder(new PlaceOrderData 
                        { CurrencySymbol = botAggregate.TradingSymbol, OrderSideId = (int)Trade.BUY, OrderTypeId = botAggregate.OrderTypeID,Quantity =botAggregate.Quantity}); //await _market.PlaceBuyOrder(botAggregate.TradingSymbol);
                            await _transaction
                                .InsertAsync(new Transactions(TransactionType.BUY, buyPrice.Price, resultbot.UserID, resultbot.ExchangeID, buyPrice.QuantityFilled));
                        break;
                        case Trade.SELL:
                        var sellPrice = await _market.PlaceOrder(new PlaceOrderData
                        { CurrencySymbol = botAggregate.TradingSymbol, OrderSideId = (int)Trade.SELL, OrderTypeId = botAggregate.OrderTypeID, Quantity = botAggregate.Quantity });
                        await _transaction
                              .InsertAsync(new Transactions(TransactionType.SELL, sellPrice.Price, resultbot.UserID, resultbot.ExchangeID, sellPrice.QuantityFilled));
                        break;
                        case Trade.HOLD:
                            _logger.LogDebug("{0} is holding : current Value {1}", botAggregate.TradingSymbol, currentPrice);
                            continue;
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
