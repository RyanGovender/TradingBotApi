using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;
using TradingBot.Objects.Transaction;

namespace TradingBot.Infrastructure.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBotOrder BotOrderRepository { get; init; }
        IRepository<Transactions> TransactionsRepository { get;init; }
        IRepository<BotOrderTransactions> BotOrderTransactionRepository { get; init; }
    }
}
