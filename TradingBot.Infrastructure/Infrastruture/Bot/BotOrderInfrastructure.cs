using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TradingBot.Infrastructure.Infrastruture.Common;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;
using TradingBot.Objects.Enums;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Bot
{
    public class BotOrderInfrastructure : BaseRepository<BotOrder>, IBotOrder
    {
        private readonly IBaseRepository _baseRepo;
       
        public BotOrderInfrastructure(IBaseRepository baseRepository, ILogger<BotOrder> logger) : base(baseRepository, logger)
        {
            _baseRepo = baseRepository;
        }

        public async Task<BotOrderAggregate> GetBotOrderAggregate(BotOrder botOrder!!)
        {
            //check if first trade
            // get last price from transactions using last buy transaction type
            var result = await _baseRepo
                .RunQuerySingleAsync<dynamic>(sqlStatement: $"SELECT tv.\"TransactionAmount\", tv.\"TransactionTypeID\", tv.\"Quantity\" FROM exchange.transaction tv " +
                "inner join exchange.BotOrderTransactions bt " +
                $"on tv.\"TransactionID\" = bt.\"TransactionID\" where bt.\"BotOrderID\" ='{botOrder.ID}' ORDER BY bt.\"ID\" DESC LIMIT 1");
            //get trading symbol using exchange ID
            var currentName = await _baseRepo
                .RunQuerySingleAsync<string>(sqlStatement: $"SELECT \"Name\" FROM exchange.Exchange e where e.\"ID\" = '{botOrder.ExchangeID}'");

            //TransactionTypeID

            Trade nextTradeAction = result?.Source is null ? Trade.BUY :
                result.Source?.TransactionTypeID == (int)TransactionType.BUY ? Trade.SELL : Trade.BUY;

            decimal quantity = result?.Source is null ? botOrder.Quantity : result?.Source?.Quantity;

            var botA = new BotOrderAggregate()
            {
                BotOrderID = botOrder.ID,
                TradeStrategyID = botOrder.TradeStrategyID,
                IsActive = botOrder.IsActive,
                TradingSymbol = currentName?.Source,
                OrderTypeID = botOrder.OrderTypeID,
                TransactionAmount = result?.Source?.TransactionAmount ?? decimal.Zero,
                Quantity = decimal.Round(quantity,4),
                IsFirstTrade = result?.Source is null,
                NextTradAction = nextTradeAction
            };

           return botA;
        }
    }
}
