using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Order;
using TradingBot.Infrastructure.Interfaces.Transaction;


namespace TradingBot.Infrastructure.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBotOrder BotOrderRepository { get; init; }
        ITransaction TransactionsRepository { get;init; }
        IBotOrderTransaction BotOrderTransactionRepository { get; init; }
        IOrderInfrastruture OrderInfrastruture { get; init; }
    }
}
