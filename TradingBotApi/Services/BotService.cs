using TradingBot.Domain.Services;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Api.Services
{
    public class BotService : IHostedService, IDisposable
    {
        private readonly ITradingBotService _tradingBotService;
        private readonly IBaseQuery _repo;
        public BotService(ITradingBotService tradingBotService, IBaseQuery repository)
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
            var result = await _repo.RunQueryAsync<BotOrder>(sqlStatement: "SELECT * FROM exchange.botorder");

            _tradingBotService.RunBot();
            //return Task.Delay(1, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
