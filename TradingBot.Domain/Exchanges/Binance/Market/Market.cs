using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Exchanges.Binance.Common;
using TradingBot.Domain.Interfaces.Market;
using Binance.Net;
using Binance.Net.Enums;
using TradingBot.Objects.Order;

namespace TradingBot.Domain.Exchanges.Binance.Market
{
    internal class Market : IMarket
    {
        private readonly IBinanceConnection _binanceConnection;

        public Market(IBinanceConnection binanceConnection)
        {
            _binanceConnection = binanceConnection;
        }

        public async Task<decimal> GetMarketPrice(string currencySymbol)
        {
        //  var assetPriceBOOK = await _binanceConnection.CreateBinanceClient()
         //     .SpotApi.ExchangeData.GetCurrentAvgPriceAsync(currencySymbol);

            var assetPrice = await _binanceConnection.CreateBinanceClient()
               .SpotApi.ExchangeData.GetPriceAsync(currencySymbol);

       //   var asset = await _binanceConnection.CreateBinanceClient()
       //       .SpotApi.ExchangeData.GetBookPriceAsync(currencySymbol);

            if (!assetPrice.Success || assetPrice.Data is null) return 0;

            return assetPrice.Data.Price; 
        }

        public async Task<PlaceOrderReturn> PlaceOrder(PlaceOrderData orderData)
        {
            var orderSide = (OrderSide)orderData.OrderSideId;
            var spotOrderType =  (SpotOrderType)orderData.OrderTypeId;

            var placeBuyOrder = await _binanceConnection.CreateBinanceClient()
             .SpotApi.Trading.PlaceOrderAsync(orderData.CurrencySymbol, orderSide, spotOrderType, orderData.Quantity);
            //time in force is something we should look at

            if (!placeBuyOrder.Success || placeBuyOrder.Data is null)
            {
                //log here
                return new PlaceOrderReturn(placeBuyOrder.Error.Message, placeBuyOrder.Error.Code, placeBuyOrder.Error.Data);
            }

            return new PlaceOrderReturn(placeBuyOrder.Data.Id, (int)placeBuyOrder.Data.Status, placeBuyOrder.Data.AverageFillPrice,
                placeBuyOrder.Data.CreateTime, placeBuyOrder.Data.Price, placeBuyOrder.Data.Quantity, placeBuyOrder.Data.QuantityFilled, placeBuyOrder.Data.StopPrice);
        }

        public async Task<dynamic> GetAllOrders(string symbol)
        {
            var getAllOrders = await _binanceConnection
                .CreateBinanceClient().SpotApi.Trading.GetOrdersAsync(symbol);

            return getAllOrders.Data;
        }

        public async Task<dynamic> QueryOrder(long id, string currentSymbol)
        {
            var getOrder = await _binanceConnection
                .CreateBinanceClient().SpotApi.Trading.GetOrderAsync(currentSymbol, id);

            return getOrder.Data;
        } 

    }
}
