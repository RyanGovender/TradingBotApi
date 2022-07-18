﻿using TradingBot.Domain.Factories.TradingFactory;
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
        private readonly ITradeFactory _tradFactory;
        private readonly ILogger<BotService> _logger;
        private readonly IUnitOfWork _uow;
        public BotService(IUnitOfWork uow, IMarket market, ITradeFactory tradeFactory, ILogger<BotService> logger)
        {
            _uow = uow;
            _market = market;
            _tradFactory = tradeFactory;
            _logger = logger;
        }

        public void Dispose()
        {
           //add some stuff here later on
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _ = BotAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public async Task BotAsync(CancellationToken cancellationToken)
        {
            var botOrder = await _uow.BotOrderRepository.GetAllAsync();
            var resultbot = botOrder.FirstOrDefault(X => X.ID == Guid.Parse("aff5a75e-d12e-4352-950a-a867512d957f"));
            bool runBot = true;
            Console.Write("Service starting");
            while (runBot)
            {
                var botAggregate = await _uow.BotOrderRepository.GetBotOrderAggregate(resultbot);
                decimal currentPrice = await _market.GetMarketPrice(botAggregate.TradingSymbol);

                if (!botAggregate?.IsOrderFilled.Value ?? false)
                {
                    var orderStatus = await _market.QueryOrder(botAggregate.BinaceOrderID.Value, botAggregate.TradingSymbol);

                    switch ((Status)orderStatus.Status)
                    {
                        case Status.Filled:
                            await _uow.BotOrderTransactionRepository
                      .InsertBotOrderTransactionAsync(botAggregate.BotOrderID, botAggregate.BinaceOrderID.Value, true, new Transactions(TransactionType.BUY, orderStatus.Price, resultbot.UserID, resultbot.ExchangeID, orderStatus.QuantityFilled));
                            break;
                        case Status.Expired:
                            var price = await _uow.TransactionsRepository.GetLastTransactionWithPriceAsync(botAggregate.BotOrderID);
                            var quantity = orderStatus.QuantityFilled + orderStatus.QuantityRemaining;
                            try
                            {
                                var ted = await _uow.BotOrderTransactionRepository
                     .InsertBotOrderTransactionAsync(botAggregate.BotOrderID, botAggregate.BinaceOrderID.Value, true, new Transactions(TransactionType.BUY, price.TransactionAmount, resultbot.UserID, resultbot.ExchangeID, quantity));
                            }
                            catch (Exception ex)
                            {

                                throw;
                            }
                          
                            break;
                    }

                    continue;
                }

                if (botAggregate.IsFirstTrade)
                {
                    var buyPrice = await _market.PlaceOrder(new PlaceOrderData
                    { CurrencySymbol = botAggregate.TradingSymbol, OrderSideId = (int)Trade.BUY, OrderTypeId = botAggregate.OrderTypeID, Quantity = botAggregate.Quantity, Price = currentPrice });
                    if (buyPrice.IsSuccess)
                        await _uow.BotOrderTransactionRepository
                         .InsertBotOrderTransactionAsync(botAggregate.BotOrderID, buyPrice.Id, buyPrice.IsOrderFilled, new Transactions(TransactionType.BUY, buyPrice.Price, resultbot.UserID, resultbot.ExchangeID, buyPrice.QuantityFilled));
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
                        { CurrencySymbol = botAggregate.TradingSymbol, OrderSideId = (int)Trade.BUY, OrderTypeId = botAggregate.OrderTypeID, Quantity = botAggregate.Quantity }); //await _market.PlaceBuyOrder(botAggregate.TradingSymbol);
                        if (buyPrice.IsSuccess)
                            await _uow.BotOrderTransactionRepository
                          .InsertBotOrderTransactionAsync(botAggregate.BotOrderID, buyPrice.Id, buyPrice.IsOrderFilled, new Transactions(TransactionType.BUY, buyPrice.Price, resultbot.UserID, resultbot.ExchangeID, buyPrice.QuantityFilled));
                        break;
                    case Trade.SELL:
                        var sellPrice = await _market.PlaceOrder(new PlaceOrderData
                        { CurrencySymbol = botAggregate.TradingSymbol, OrderSideId = (int)Trade.SELL, OrderTypeId = botAggregate.OrderTypeID, Quantity = botAggregate.Quantity });
                        if (sellPrice.IsSuccess)
                            await _uow.BotOrderTransactionRepository
                        .InsertBotOrderTransactionAsync(botAggregate.BotOrderID, sellPrice.Id, sellPrice.IsOrderFilled, new Transactions(TransactionType.BUY, sellPrice.Price, resultbot.UserID, resultbot.ExchangeID, sellPrice.QuantityFilled));
                        break;
                    case Trade.HOLD:
                        _logger.LogDebug("{0} is holding : current Value {1}", botAggregate.TradingSymbol, currentPrice);
                        break;
                        //continue;
                }

                await Task.Delay(10000, cancellationToken);
            }
            Console.Write("Service closing...");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
