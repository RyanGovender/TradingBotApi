using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingBot.Domain.Interfaces.Market;

namespace TradingBot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMarket _market;

        public OrderController(IMarket market)
        {
            _market = market;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(string symbol)
        {
            var result = await _market.GetAllOrders(symbol);

            return Ok(result);
        }
    }
}
