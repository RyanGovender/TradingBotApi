using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Infrastructure.Infrastruture.Common;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Exchange;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Bot
{
    public class ExchangeInfrastructure : BaseRepository<Symbol>, IRepository<Symbol>
    {
        public ExchangeInfrastructure(IBaseRepository baseRepository, ILogger<Symbol> logger) : base(baseRepository, logger)
        {
        }
    }
}
