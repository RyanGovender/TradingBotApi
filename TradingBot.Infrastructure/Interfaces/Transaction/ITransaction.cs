using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Transaction;

namespace TradingBot.Infrastructure.Interfaces.Transaction
{
    public interface ITransaction: IRepository<Transactions>
    {
        Task<Transactions> GetTransactionByBinanceID(long binanceID);
        Task<Transactions> GetLastTransactionWithPriceAsync(Guid BotOrderID);
        Task<bool> HasBotOrderAsync(Guid BotOrderID);
        Task<LatestTransaction> GetLatestTransactionAsync(Guid BotOrderID);
    }
}
