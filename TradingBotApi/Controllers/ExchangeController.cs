using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingBot.Domain.Interfaces.Exchange;

namespace TradingBot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchange _exchange;
        public ExchangeController(IExchange exchange)
        {
            _exchange = exchange;
        }

        [HttpGet]
        public async Task<IActionResult> GetExchangeData()
        {
            var exchangeData = await _exchange.GetExchangeData();

            return Ok(exchangeData);
        }
    }
}
