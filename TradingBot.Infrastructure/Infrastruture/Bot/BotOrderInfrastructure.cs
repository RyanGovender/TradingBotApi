using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TradingBot.Infrastructure.Infrastruture.Common;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Infrastructure.Interfaces.Transaction;
using TradingBot.Objects.Bot;
using TradingBot.Objects.Enums;
using TradingBot.Objects.Exchange;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Bot
{
    public class BotOrderInfrastructure : BaseRepository<BotOrder>, IBotOrder
    {
        private readonly IBaseRepository _baseRepo;
        private readonly ITransaction _transactionRepo;
        private readonly IRepository<Symbol> _symbolRepo;

        public BotOrderInfrastructure(IBaseRepository baseRepository, ILogger<BotOrder> logger, IRepository<Symbol> symbolRepo, ITransaction transaction) : base(baseRepository, logger)
        {
            _baseRepo = baseRepository;
            _symbolRepo = symbolRepo;
            _transactionRepo = transaction;
        }

        public async Task<BotOrderAggregate> GetBotOrderAggregate(BotOrder botOrder!!)
        {
            //check if first trade
            // get last price from transactions using last buy transaction type
            var symbolDetailsTask = _symbolRepo.FindAsync(botOrder.ExchangeID);
            var hasTransactionTask = _transactionRepo.HasBotOrderAsync(botOrder.ID);
          
            //get trading symbol using exchange ID
            await Task.WhenAll(symbolDetailsTask, hasTransactionTask);

            var symbolDetails = await symbolDetailsTask;
            var hasTransaction = await hasTransactionTask;

            if (symbolDetails == null)
                throw new NullReferenceException("Data not found.");

            if (!hasTransaction)
            {
                return new(botOrder.ID, botOrder.TradeStrategyID, botOrder.IsActive, symbolDetails.Name, botOrder.OrderTypeID, botOrder.Quantity);
            }

            var botOrderTransaction = await _transactionRepo.GetLatestTransactionAsync(botOrder.ID);

            if (botOrderTransaction is null)
                return new();

            return new()
            {
                BotOrderID = botOrder.ID,
                TradeStrategyID = botOrder.TradeStrategyID,
                IsActive = botOrder.IsActive,
                TradingSymbol = symbolDetails.Name,
                OrderTypeID = botOrder.OrderTypeID,
                TransactionAmount = botOrderTransaction.TransactionAmount,
                Quantity = decimal.Round(botOrderTransaction.Quantity, 4),
                IsFirstTrade = false,
                IsOrderFilled = botOrderTransaction.IsOrderFilled, 
                BinaceOrderID = botOrderTransaction.BinanceOrderID,
                NextTradAction = botOrderTransaction.TransactionTypeID == (int)TransactionType.BUY ? Trade.SELL : Trade.BUY
            };
        }
    }
}
