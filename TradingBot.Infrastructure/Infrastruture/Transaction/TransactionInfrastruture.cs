using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Transaction
{
    public class TransactionInfrastruture : IRepository<Transactions>
    {
        private readonly IBaseRepository _baseRepo;
        public TransactionInfrastruture(IBaseRepository baseRepository)
        {
            _baseRepo = baseRepository;
        }
        public Task<bool> DeleteAsync(Transactions entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<Transactions> FindAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transactions>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Transactions> InsertAsync(Transactions data!!)
        {
            var result = await _baseRepo.InsertAsync(data);

            //will be removed
            if (result.IsSuccess)
            {
               var res =  await _baseRepo.InsertAsync(new BotOrderTransactions {BotOrderID = Guid.Parse("12cb01e5-ecf8-4094-be3f-cc7cdb54a609"), TransactionID = data.TransactionID });
            }

            return data;
        }

        public Task<Transactions> UpdateAsync(Transactions data)
        {
            throw new NotImplementedException();
        }
    }
}
