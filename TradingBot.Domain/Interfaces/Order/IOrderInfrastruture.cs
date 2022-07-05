using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Domain.Interfaces.Order
{
    public interface IOrderInfrastruture
    {
        Task<dynamic> GetAllOrdersForSymbolAsync(string symbol);
    }
}
