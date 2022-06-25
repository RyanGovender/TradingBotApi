using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;

namespace TradingBot.Infrastructure.Interfaces.Bot
{
    public interface IBotOrder : IRepository<BotOrder>
    {
        Task<BotOrderAggregate> GetBotOrderAggregate(BotOrder botOrder);
    }
}
