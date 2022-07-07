using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Bot
{
    internal class BotOrderTransactionInfrastructure : IRepository<BotOrderTransactions>
    {
        private readonly IBaseRepository _baseRepo;

        public BotOrderTransactionInfrastructure(IBaseRepository baseRepo)
        {
            _baseRepo = baseRepo;
        }

        public Task<bool> DeleteAsync(BotOrderTransactions entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<BotOrderTransactions> FindAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BotOrderTransactions>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BotOrderTransactions> InsertAsync(BotOrderTransactions data!!)
        {
            var result = await _baseRepo.InsertAsync(data);

            if (!result.IsSuccess) throw new Exception(result.Exception.Message);
            //return type needs to be something more meaningful.
            return data;
        }

        public Task<BotOrderTransactions> UpdateAsync(BotOrderTransactions data)
        {
            throw new NotImplementedException();
        }
    }
}
