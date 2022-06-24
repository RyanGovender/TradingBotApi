using TradingBot.Domain.Services;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Api.Services
{
    public class BotService : IHostedService, IDisposable
    {
        private readonly ITradingBotService _tradingBotService;
        private readonly IBaseRepository _repo;
        public BotService(ITradingBotService tradingBotService, IBaseRepository repository)
        {
            _tradingBotService = tradingBotService;
            _repo = repository;
        }

        public void Dispose()
        {
           //add some stuff here later on
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //test please remove
            //var result = await _repo.GetAllAsync();
            var result = await _repo.RunQuerySingleAsync<decimal>(sqlStatement: $"SELECT tv.\"TransactionAmount\" FROM exchange.transaction tv " +
                "inner join exchange.BotOrderTransactions bt " +
                $"on tv.\"ID\" = bt.\"TransactionID\" where bt.\"BotOrderID\" ='03fc7c7a-f600-4e12-9f77-659730c70dd4'");

            var result3 = await _repo.RunQuerySingleAsync<string>(sqlStatement: $"SELECT \"Name\" FROM exchange.Exchange e where e.\"ID\" = '9af09b14-11fd-4ed4-b17b-1bb9a569b3fc'");

            var resultS = await _repo
            .RunQuerySingleAsync<dynamic>(sqlStatement: $"SELECT tv.\"TransactionAmount\", tv.\"TransactionTypeID\" FROM exchange.transaction tv " +
            "inner join exchange.BotOrderTransactions bt " +
            $"on tv.\"ID\" = bt.\"TransactionID\" where bt.\"BotOrderID\" ='03fc7c7a-f600-4e12-9f77-659730c70dd4'");
            var testt = resultS.Source.TransactionTypeID;
            //get trading symbol using exchange ID
            var currentName = await _repo
                .RunQuerySingleAsync<string>(sqlStatement: $"SELECT \"Name\" FROM exchange.Exchange e where e.\"ID\" = '9af09b14-11fd-4ed4-b17b-1bb9a569b3fc'");

            _tradingBotService.RunBot();
            //return Task.Delay(1, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
