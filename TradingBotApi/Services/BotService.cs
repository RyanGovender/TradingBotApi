using TradingBot.Domain.Factories.TradingFactory;
using TradingBot.Domain.Interfaces.Account;
using TradingBot.Domain.Interfaces.Market;
using TradingBot.Domain.Objects;
using TradingBot.Domain.Services;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Infrastructure.Interfaces.UnitOfWork;
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
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Transactions> _transaction;
        public BotService(IUnitOfWork uow, IMarket market, IAccount account, ITradeFactory tradeFactory, ILogger<BotService> logger, IRepository<Transactions> repository)
        {
            _uow = uow;
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
            var botOrder = await _uow.BotOrderRepository.GetAllAsync();
            var resultbot = botOrder.FirstOrDefault(X=>X.ID == Guid.Parse("12cb01e5-ecf8-4094-be3f-cc7cdb54a609"));
            bool runBot = true;
            //var sellPriceS = await _market.PlaceOrder(new PlaceOrderData
            //{ CurrencySymbol = "BTCBUSD", OrderSideId = (int)Trade.SELL, OrderTypeId = 1, Quantity = 1.0m });

            while (runBot)
            {
                var botAggregate = await _uow.BotOrderRepository.GetBotOrderAggregate(resultbot);
                decimal currentPrice = await _market.GetMarketPrice(botAggregate.TradingSymbol);
                Result transactionInsertResult = Result.ERROR;

                if (botAggregate.IsFirstTrade)
                {
                    var buyPrice = await _market.PlaceOrder(new PlaceOrderData
                    { CurrencySymbol = botAggregate.TradingSymbol, OrderSideId = (int)Trade.BUY, OrderTypeId = botAggregate.OrderTypeID, Quantity = botAggregate.Quantity, Price = currentPrice }); //await _market.PlaceBuyOrder(botAggregate.TradingSymbol);
                    if(buyPrice.IsSuccess)
                         transactionInsertResult = await _uow.BotOrderTransactionRepository
                        .InsertBotOrderTransactionAsync(new Transactions(TransactionType.BUY, buyPrice.Price, resultbot.UserID, resultbot.ExchangeID, buyPrice.QuantityFilled), botAggregate.BotOrderID);
                    continue;
                }

                botAggregate.Quantity = botAggregate.NextTradAction == Trade.BUY ?
                  decimal.Round((botAggregate.TransactionAmount / currentPrice) * botAggregate.Quantity, 4) :
                  botAggregate.Quantity;

                var nextOperation = _tradFactory
                    .RunFactory(TradeStrategy.SIMPLE_TRADE, new MarketData { MarketId = botAggregate.TradingSymbol, CurrentPrice = currentPrice, PurchasePrice = botAggregate.TransactionAmount, NextAction = botAggregate.NextTradAction });

                switch (nextOperation)
                    {
                        case Trade.BUY:
                        var buyPrice = await _market.PlaceOrder(new PlaceOrderData 
                        { CurrencySymbol = botAggregate.TradingSymbol, OrderSideId = (int)Trade.BUY, OrderTypeId = botAggregate.OrderTypeID,Quantity =botAggregate.Quantity}); //await _market.PlaceBuyOrder(botAggregate.TradingSymbol);
                        if (buyPrice.IsSuccess)
                            transactionInsertResult = await _uow.BotOrderTransactionRepository
                         .InsertBotOrderTransactionAsync(new Transactions(TransactionType.BUY, buyPrice.Price, resultbot.UserID, resultbot.ExchangeID, buyPrice.QuantityFilled), botAggregate.BotOrderID);
                        break;
                        case Trade.SELL:
                        var sellPrice = await _market.PlaceOrder(new PlaceOrderData
                        { CurrencySymbol = botAggregate.TradingSymbol, OrderSideId = (int)Trade.SELL, OrderTypeId = botAggregate.OrderTypeID, Quantity = botAggregate.Quantity });
                        if (sellPrice.IsSuccess)
                            transactionInsertResult = await _uow.BotOrderTransactionRepository
                         .InsertBotOrderTransactionAsync(new Transactions(TransactionType.SELL, sellPrice.Price, resultbot.UserID, resultbot.ExchangeID, sellPrice.QuantityFilled), botAggregate.BotOrderID);
                        break;
                        case Trade.HOLD:
                            _logger.LogDebug("{0} is holding : current Value {1}", botAggregate.TradingSymbol, currentPrice);
                        break;
                            //continue;
                    }
              
                await Task.Delay(10000, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
