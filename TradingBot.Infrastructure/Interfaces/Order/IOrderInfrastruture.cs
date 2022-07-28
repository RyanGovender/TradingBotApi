using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Objects.Bot;
using TradingBot.Objects.Enums;
using TradingBot.Objects.Order;

namespace TradingBot.Infrastructure.Interfaces.Order
{
    public interface IOrderInfrastruture
    {
        Task CheckIncompleteOrder(BotOrderAggregate botOrder, Guid userID, Guid exchangeID);
        Task<Result> PlaceOrder(PlaceOrderData orderData, Guid userID, Guid exchangeID, Guid botOrderID);
    }
}
