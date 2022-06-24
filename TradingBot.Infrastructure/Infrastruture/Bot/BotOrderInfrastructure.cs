using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Bot
{
    public class BotOrderInfrastructure : IRepository<BotOrder>
    {
        private readonly IBaseRepository _baseRepo;
        public BotOrderInfrastructure(IBaseRepository baseRepository)
        {
            _baseRepo = baseRepository;
        }

        public Task<bool> DeleteAsync(BotOrder entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<BotOrder> FindAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BotOrder>> GetAllAsync()
        {
            var result = await _baseRepo.GetAllAsync<BotOrder>();

            if(!result.IsSuccess || result?.Source is null) return Enumerable.Empty<BotOrder>();

            return result.Source;
        }

        public Task<BotOrder> InsertAsync(BotOrder data)
        {
            throw new NotImplementedException();
        }

        public Task<BotOrder> UpdateAsync(BotOrder data)
        {
            throw new NotImplementedException();
        }

        public async Task<BotOrderAggregate> GetBotOrderAggregate(BotOrder botOrder!!)
        {
            //check if first trade
            // get last price from transactions using last buy transaction type
            var result = await _baseRepo
                .RunQuerySingleAsync<dynamic>(sqlStatement: $"SELECT tv.\"TransactionAmount\", tv.\"TransactionTypeID\" FROM exchange.transaction tv " +
                "inner join exchange.BotOrderTransactions bt " +
                $"on tv.\"ID\" = bt.\"TransactionID\" where bt.\"BotOrderID\" ='{botOrder.ID}'");
            //get trading symbol using exchange ID
            var currentName = await _baseRepo
                .RunQuerySingleAsync<string>(sqlStatement: $"SELECT \"Name\" FROM exchange.Exchange e where e.\"ID\" = '{botOrder.ExchangeID}'");

            //TransactionTypeID

            var botA = new BotOrderAggregate()
            {
                BotOrderID = botOrder.ID,
                TradeStrategyID = botOrder.TradeStrategyID,
                IsActive = botOrder.IsActive,
                TradingSymbol = currentName?.Source,
                OrderTypeID = botOrder.OrderTypeID,
                PurchasePrice = result.Source.TransactionAmount,
                Quantity = botOrder.Quantity,
                IsFirstTrade = result?.Source is null
            };

           return botA;
        }
    }
}
