using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingBot.Domain.Interfaces.Account;

namespace TradingBot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _account;

        public AccountController(IAccount account)
        {
            _account =  account;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountInformation()
        {
            var accountInformation = await _account.GetAccountInformation();

            return Ok(accountInformation);
        }
    }
}
