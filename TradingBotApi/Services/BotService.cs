using TradingBot.Domain.Services;

namespace TradingBot.Api.Services
{
    public class BotService : IHostedService, IDisposable
    {
        private readonly ITradingBotService _tradingBotService;
        public BotService(ITradingBotService tradingBotService)
        {
            _tradingBotService = tradingBotService;
        }

        public void Dispose()
        {
           //add some stuff here later on
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _tradingBotService.RunBot();
            return Task.Delay(1, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
