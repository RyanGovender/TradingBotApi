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
               var res =  await _baseRepo.InsertAsync(new BotOrderTransactions {BotOrderID = Guid.Parse("03fc7c7a-f600-4e12-9f77-659730c70dd4"), TransactionID = data.TransactionID });
            }

            return data;
        }

        public Task<Transactions> UpdateAsync(Transactions data)
        {
            throw new NotImplementedException();
        }
    }
}
