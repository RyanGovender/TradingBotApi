using Binance.Net.Clients;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Exchanges.Binance.Common;
using TradingBot.Domain.Interfaces.Account;

namespace TradingBot.Domain.Exchanges.Binance.Account
{
    internal class Account : IAccount
    {
        private readonly IBinanceConnection _binanceConnection;
        public Account(IBinanceConnection binanceConnection)
        {
            _binanceConnection = binanceConnection;
        }

        public async Task<double> GetBalance()
        {
            var getAccountInfo = await _binanceConnection.CreateBinanceClient().SpotApi.Account.GetAccountInfoAsync();

            if (!getAccountInfo.Success) return 0.00;

            return (double)getAccountInfo.Data.Balances.First().Total;
        }

        public async Task<BinanceAccountInfo> GetAccountInformation()
        {
            var getAccountInfo = await _binanceConnection.CreateBinanceClient()
                .SpotApi.Account.GetAccountInfoAsync();

            if (!getAccountInfo.Success) throw new Exception(getAccountInfo.Error?.Message);

            return getAccountInfo.Data;
        }
    }
}
