using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Infrastructure.Infrastruture.Common;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Enums;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Bot
{
    internal class BotOrderTransactionInfrastructure : BaseRepository<BotOrderTransactions>, IRepository<BotOrderTransactions>
    {
        public BotOrderTransactionInfrastructure(IBaseRepository baseRepository, ILogger<BotOrderTransactions> logger) : 
            base(baseRepository, logger)
        {
        }
    }
}
