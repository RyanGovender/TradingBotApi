using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Objects.Order;

namespace TradingBot.Domain.Interfaces.Market
{
    public interface IMarket
    {
        Task<dynamic> GetAllOrders(string symbol);
        Task<decimal> GetMarketPrice(string currencySymbol);
        Task<PlaceOrderReturn> PlaceOrder(PlaceOrderData orderData);
        Task<dynamic> QueryOrder(long id, string currentSymbol);
    }
}
