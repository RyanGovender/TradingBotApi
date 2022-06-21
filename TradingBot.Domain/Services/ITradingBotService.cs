using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Domain.Services
{
    public interface ITradingBotService
    {
        Task RunBot();
    }
}
