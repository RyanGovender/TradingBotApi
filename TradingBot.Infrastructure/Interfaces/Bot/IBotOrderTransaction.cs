using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Enums;
using TradingBot.Objects.Transaction;

namespace TradingBot.Infrastructure.Interfaces.Bot
{
    public interface IBotOrderTransaction : IRepository<BotOrderTransactions>
    {
        Task<Result> InsertBotOrderTransactionAsync(Transactions transactions, Guid botOrderID);
    }
}
