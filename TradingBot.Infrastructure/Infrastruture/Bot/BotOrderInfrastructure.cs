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
            var symbolDetailsTask = _symbolRepo.FindAsync(botOrder.ExchangeID);
            //check if first trade
            // get last price from transactions using last buy transaction type
            var botOrderTransactionTask = _baseRepo.RunQuerySingleAsync<LatestTransaction>("exchange.getlatesttransactionusingbotorderid", parameters: new { botorderid = botOrder.ID });

            //get trading symbol using exchange ID

            await Task.WhenAll(symbolDetailsTask, botOrderTransactionTask);

            var symbolDetails = await symbolDetailsTask;
            var botOrderTransaction = await botOrderTransactionTask;

            //TransactionTypeID
            if (symbolDetails == null || botOrderTransaction == null)
                throw new NullReferenceException("Data not found.");

            Trade nextTradeAction = botOrderTransaction?.Source is null ? Trade.BUY :
                botOrderTransaction.Source?.TransactionTypeID == (int)TransactionType.BUY ? Trade.SELL : Trade.BUY;

            decimal quantity = botOrderTransaction?.Source is null ? botOrder.Quantity : botOrderTransaction.Source.Quantity;

            var botA = new BotOrderAggregate()
            {
                BotOrderID = botOrder.ID,
                TradeStrategyID = botOrder.TradeStrategyID,
                IsActive = botOrder.IsActive,
                TradingSymbol = symbolDetails.Name,
                OrderTypeID = botOrder.OrderTypeID,
                TransactionAmount = botOrderTransaction?.Source?.TransactionAmount ?? decimal.Zero,
                Quantity = decimal.Round(quantity,4),
                IsFirstTrade = botOrderTransaction?.Source is null,
                IsOrderFilled = botOrderTransaction?.Source.IsOrderFilled, 
                BinaceOrderID = botOrderTransaction?.Source.BinanceOrderID,
                NextTradAction = nextTradeAction
            };

           return botA;
        }
    }
}
