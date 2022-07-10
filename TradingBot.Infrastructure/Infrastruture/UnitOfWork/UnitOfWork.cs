using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Infrastructure.Interfaces.UnitOfWork;
using TradingBot.Objects.Transaction;

namespace TradingBot.Infrastructure.Infrastruture.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IBotOrder BotOrderRepository { get; init; }
        public IRepository<Transactions> TransactionsRepository { get ; init ; }
        public IBotOrderTransaction BotOrderTransactionRepository { get ; init ; }

        public UnitOfWork(IBotOrder botOrderRepository, IRepository<Transactions> transactionsRepository, IBotOrderTransaction botOrderTransactionRepository)
        {
            BotOrderRepository = botOrderRepository;
            TransactionsRepository = transactionsRepository;
            BotOrderTransactionRepository = botOrderTransactionRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
