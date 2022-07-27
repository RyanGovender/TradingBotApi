using Microsoft.Extensions.Logging;
using TradingBot.Infrastructure.Infrastruture.Common;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Infrastructure.Interfaces.Transaction;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Transaction
{
    public class TransactionInfrastruture : BaseRepository<Transactions>, ITransaction
    {
        private readonly IBaseRepository _baseRepo;
        public TransactionInfrastruture(IBaseRepository baseRepository, ILogger<Transactions> logger) 
            : base(baseRepository, logger)
        {
            _baseRepo = baseRepository;
        }

        public async Task<Transactions> GetTransactionByBinanceID(long binanceID)
        {
            var result = await _baseRepo.
                RunQuerySingleAsync<Transactions>(Function.GetBotOrderTransactionUsingBinanceID, parameters: new { binanceid = binanceID });

            if (!result.IsSuccess || result.Source==null)
                throw new(result?.Exception?.Message);

            return result?.Source ?? new();
        }

        public async Task<Transactions> GetLastTransactionWithPriceAsync(Guid BotOrderID)
        {
            var result = await _baseRepo.
                RunQuerySingleAsync<Transactions>(Function.GetBotOrderTransactionUsingID, parameters: new { botorderid = BotOrderID });

            if (!result.IsSuccess || result.Source == null)
                throw new(result?.Exception?.Message);

            return result?.Source ?? new();
        }

        public async Task<LatestTransaction> GetLatestTransactionAsync(Guid BotOrderID)
        {
            var botOrderTransactionTask = await _baseRepo
                    .RunQuerySingleAsync<LatestTransaction>(Function.GetLastestTransactonUsingBotOrderID, parameters: new { botorderid = BotOrderID });

            if (!botOrderTransactionTask.IsSuccess || botOrderTransactionTask.Source == null)
                throw new(botOrderTransactionTask?.Exception?.Message);

            return botOrderTransactionTask.Source;
        }

        public async Task<bool> HasBotOrderAsync(Guid BotOrderID)
        {
            var BotOrderTransactionTask = await _baseRepo.RunQuerySingleAsync<long>(Function.HasBotOrderTransaction, parameters: new { botorderid = BotOrderID });

            return BotOrderTransactionTask.IsSuccess && BotOrderTransactionTask.Source > 0;
        }
    }
}
