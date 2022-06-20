using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Exchanges.Binance.Common;
using TradingBot.Domain.Interfaces.Market;
using Binance.Net;
using Binance.Net.Enums;

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
            var assetPrice = await _binanceConnection.CreateBinanceClient()
                .CoinFuturesApi.ExchangeData.GetPricesAsync("BTCUSDT");

            if (!assetPrice.Success || assetPrice.Data is null) return 0;

            return assetPrice.Data.FirstOrDefault().Price;
        }
        //refator later on
        public async Task<decimal> PlaceBuyOrder(string currencySymbol)
        {
            var placeBuyOrder = await _binanceConnection.CreateBinanceClient()
               .CoinFuturesApi.Trading.PlaceOrderAsync(currencySymbol, OrderSide.Buy, FuturesOrderType.Market, 1);

            if (!placeBuyOrder.Success || placeBuyOrder.Data is null) return 0;

            return placeBuyOrder.Data.Price;
        }

        public async Task<decimal> PlaceSellOrder(string currencySymbol)
        {
            var placeBuyOrder = await _binanceConnection.CreateBinanceClient()
              .CoinFuturesApi.Trading.PlaceOrderAsync(currencySymbol, OrderSide.Sell, FuturesOrderType.Market, 1);

            if (!placeBuyOrder.Success || placeBuyOrder.Data is null) return 0;

            return placeBuyOrder.Data.Price;
        }
    }
}
