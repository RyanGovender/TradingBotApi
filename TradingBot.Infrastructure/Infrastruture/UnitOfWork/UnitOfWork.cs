using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Order;
using TradingBot.Infrastructure.Interfaces.Transaction;
using TradingBot.Infrastructure.Interfaces.UnitOfWork;

namespace TradingBot.Infrastructure.Infrastruture.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IBotOrder BotOrderRepository { get; init; }
        public ITransaction TransactionsRepository { get ; init ; }
        public IBotOrderTransaction BotOrderTransactionRepository { get ; init ; }
        public IOrderInfrastruture OrderInfrastruture { get; init ; }

        public UnitOfWork(IBotOrder botOrderRepository, ITransaction transactionsRepository, IBotOrderTransaction botOrderTransactionRepository, IOrderInfrastruture orderInfrastruture)
        {
            BotOrderRepository = botOrderRepository;
            TransactionsRepository = transactionsRepository;
            BotOrderTransactionRepository = botOrderTransactionRepository;
            OrderInfrastruture = orderInfrastruture;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
