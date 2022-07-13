using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Enums;
using TradingBot.Objects.Order;
using TradingBot.Objects.Transaction;

namespace TradingBot.Infrastructure.Interfaces.Bot
{
    public interface IBotOrderTransaction : IRepository<BotOrderTransactions>
    {
        Task<BotOrderTransactions> GetBotOrderTransactionsByBinanceID(long binanceID);
        Task<Result> InsertBotOrderTransactionAsync(Transactions transactions, PlaceOrderReturn placeOrderReturn, Guid botOrderID);
    }
}
