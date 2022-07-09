using Microsoft.Extensions.Logging;
using TradingBot.Infrastructure.Infrastruture.Common;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Bot
{
    public class BotOrderTransactionInfrastructure : BaseRepository<BotOrderTransactions>, IRepository<BotOrderTransactions>
    {
        public BotOrderTransactionInfrastructure(IBaseRepository baseRepository, ILogger<BotOrderTransactions> logger) : 
            base(baseRepository, logger)
        {
        }
    }
}
