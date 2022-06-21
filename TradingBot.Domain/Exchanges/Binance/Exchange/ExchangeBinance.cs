using Binance.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Exchanges.Binance.Common;
using TradingBot.Domain.Interfaces.Exchange;

namespace TradingBot.Domain.Exchanges.Binance.Exchange
{
    internal class ExchangeBinance: IExchange
    {
        private readonly IBinanceConnection _binanceConnection;
        public ExchangeBinance(IBinanceConnection binanceConnection)
        {
            _binanceConnection = binanceConnection;
        }

        public async Task<BinanceExchangeInfo> GetExchangeData()
        {
            var exchangeData = await _binanceConnection.CreateBinanceClient()
                .SpotApi.ExchangeData.GetExchangeInfoAsync();

            if (!exchangeData.Success) throw new Exception(exchangeData.Error?.Message);

            return exchangeData.Data;
        }
    }
}
