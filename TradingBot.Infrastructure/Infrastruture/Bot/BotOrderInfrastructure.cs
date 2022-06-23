using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Bot
{
    public class BotOrderInfrastructure : IRepository<BotOrder>
    {
        private readonly IBaseRepository _baseRepo;
        public BotOrderInfrastructure(IBaseRepository baseRepository)
        {
            _baseRepo = baseRepository;
        }

        public Task<bool> DeleteAsync(BotOrder entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<BotOrder> FindAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BotOrder>> GetAllAsync()
        {
            var result = await _baseRepo.GetAllAsync<BotOrder>();

            if(!result.IsSuccess || result?.Source is null) return Enumerable.Empty<BotOrder>();

            return result.Source;
        }

        public Task<BotOrder> InsertAsync(BotOrder data)
        {
            throw new NotImplementedException();
        }

        public Task<BotOrder> UpdateAsync(BotOrder data)
        {
            throw new NotImplementedException();
        }

        public Task<BotOrderAggregate> GetBotOrderAggregate(BotOrder botOrder)
        {
            throw new NotImplementedException();
        }
    }
}
