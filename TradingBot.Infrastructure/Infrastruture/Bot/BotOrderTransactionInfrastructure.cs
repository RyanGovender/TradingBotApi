using Microsoft.Extensions.Logging;
using TradingBot.Infrastructure.Infrastruture.Common;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Enums;
using TradingBot.Objects.Order;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Bot
{
    public class BotOrderTransactionInfrastructure : BaseRepository<BotOrderTransactions>, IBotOrderTransaction
    {

        private readonly IRepository<Transactions> _transactionRepo;
        private readonly IBaseRepository _baseRepo;

        public BotOrderTransactionInfrastructure(IBaseRepository baseRepository, ILogger<BotOrderTransactions> logger, IRepository<Transactions> transactionRepo) : 
            base(baseRepository, logger)
        {
            _transactionRepo = transactionRepo;
            _baseRepo = baseRepository;
        }

        public async Task<BotOrderTransactions> GetBotOrderTransactionsByBinanceID(long binanceID)
        {
            var result = await _baseRepo.
                RunQuerySingleAsync<BotOrderTransactions>(sqlStatement: $"CALL exchange.getBotOrderTransactionFormBinanceID({binanceID})", parameters: new { BinanceID = binanceID });

            return result.Source;
        }

        public async Task<Result> InsertBotOrderTransactionAsync(Transactions transactions!!, PlaceOrderReturn placeOrderReturn, Guid botOrderID)
        {
            var transactionResult = await _transactionRepo.InsertAsync(transactions);

            if(transactionResult != Result.SUCCESSFUL)
            {
                //log error here
                return transactionResult;
            }

            var result = await base.InsertAsync(new BotOrderTransactions { BotOrderID = botOrderID, TransactionID = transactions.TransactionID, BinanceOrderID = placeOrderReturn.Id, IsOrderFilled = placeOrderReturn.IsOrderFilled });

            return result;
        }
    }
}
