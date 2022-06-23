using TradingBot.Domain.Services;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;

namespace TradingBot.Api.Services
{
    public class BotService : IHostedService, IDisposable
    {
        private readonly ITradingBotService _tradingBotService;
        private readonly IRepository<BotOrder> _repo;
        public BotService(ITradingBotService tradingBotService, IRepository<BotOrder> repository)
        {
            _tradingBotService = tradingBotService;
            _repo = repository;
        }

        public void Dispose()
        {
           //add some stuff here later on
        }

        public  Task StartAsync(CancellationToken cancellationToken)
        {
            //test please remove
            //var result = await _repo.GetAllAsync();

            _tradingBotService.RunBot();
            return Task.Delay(1, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
