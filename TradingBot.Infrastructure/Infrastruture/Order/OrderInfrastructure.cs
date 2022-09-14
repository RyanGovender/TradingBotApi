using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Interfaces.Market;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Order;
using TradingBot.Infrastructure.Interfaces.Transaction;
using TradingBot.Objects.Bot;
using TradingBot.Objects.Enums;
using TradingBot.Objects.Order;
using TradingBot.Objects.Transaction;

namespace TradingBot.Infrastructure.Infrastruture.Order
{
    public class OrderInfrastructure : IOrderInfrastruture
    {
        private readonly IMarket _market;
        private readonly IBotOrderTransaction _botOrderTransaction;
        private readonly ITransaction _transactionRepository;

        public OrderInfrastructure(IMarket market, IBotOrderTransaction botOrderTransaction, ITransaction transaction)
        {
            _market = market;
            _botOrderTransaction = botOrderTransaction;
            _transactionRepository = transaction;
        }

        public async Task<Result> PlaceOrder(PlaceOrderData orderData, Guid userID, Guid exchangeID, Guid botOrderID)
        {
            var buyPrice = await _market.PlaceOrder(orderData);

            if (!buyPrice.IsSuccess)
                return Result.FAILED;

           return await _botOrderTransaction
                 .InsertBotOrderTransactionAsync(botOrderID, buyPrice.Id, buyPrice.IsOrderFilled, new Transactions(TransactionType.BUY, buyPrice.Price, userID, exchangeID, buyPrice.QuantityFilled));
        }

        public async Task CheckIncompleteOrder(BotOrderAggregate botOrder, Guid userID, Guid exchangeID)
        {
            if (!botOrder.BinaceOrderID.HasValue)
            {
                throw new("");
            }

            var orderStatus = await _market.QueryOrder(botOrder.BinaceOrderID.Value, botOrder.TradingSymbol);

            switch ((Status)orderStatus.Status)
            {
                case Status.Filled:
                    _= _botOrderTransaction
                   .InsertBotOrderTransactionAsync(botOrder.BotOrderID, botOrder.BinaceOrderID.Value, true, new Transactions(TransactionType.BUY, orderStatus.Price, userID, exchangeID, orderStatus.QuantityFilled));
                    break;
                case Status.Expired:
                    var price = await _transactionRepository.GetLastTransactionWithPriceAsync(botOrder.BotOrderID);
                    var quantity = orderStatus.QuantityFilled + orderStatus.QuantityRemaining;
                    _= _botOrderTransaction
                    .InsertBotOrderTransactionAsync(botOrder.BotOrderID, botOrder.BinaceOrderID.Value, true, new Transactions(TransactionType.BUY, price.TransactionAmount, userID, exchangeID, quantity));
                    break;
            }
        }
    }
}
