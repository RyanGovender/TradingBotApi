using Microsoft.Extensions.Logging;
using TradingBot.Infrastructure.Infrastruture.Common;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Infrastructure.Interfaces.Transaction;
using TradingBot.Objects.Enums;
using TradingBot.Objects.Order;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Bot
{
    public class BotOrderTransactionInfrastructure : BaseRepository<BotOrderTransactions>, IBotOrderTransaction
    {
        private readonly ITransaction _transactionRepo;

        public BotOrderTransactionInfrastructure(IBaseRepository baseRepository, ILogger<BotOrderTransactions> logger, ITransaction transactionRepo) : 
            base(baseRepository, logger)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<Result> InsertBotOrderTransactionAsync(Guid botOrderID, long binanceOrderID, bool isOrderFilled, Transactions transactions!!)
        {
            var transactionResult = await _transactionRepo.InsertAsync(transactions);

            if(transactionResult != Result.SUCCESSFUL)
            {
                //log error here
                return transactionResult;
            }
            
            var result = await base.InsertAsync(new BotOrderTransactions { BotOrderID = botOrderID, TransactionID = transactions.TransactionID, BinanceOrderID = binanceOrderID, IsOrderFilled = isOrderFilled });

            return result;
        }
    }
}
