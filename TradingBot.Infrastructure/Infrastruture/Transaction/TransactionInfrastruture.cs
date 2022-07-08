using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Infrastructure.Infrastruture.Common;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Enums;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Transaction
{
    public class TransactionInfrastruture : BaseRepository<Transactions>, IRepository<Transactions>
    {
        public TransactionInfrastruture(IBaseRepository baseRepository, ILogger<Transactions> logger) : base(baseRepository, logger)
        {
        }
       
        //public async Task<Result> InsertAsync(Transactions data!!)
        //{
        //    var result = await _baseRepo.InsertAsync(data);

        //    //will be removed
        //    if (result.IsSuccess)
        //    {
        //       var res =  await _baseRepo.InsertAsync(new BotOrderTransactions {BotOrderID = Guid.Parse("12cb01e5-ecf8-4094-be3f-cc7cdb54a609"), TransactionID = data.TransactionID });
        //    }

        //    return Result.SUCCESSFUL;
        //}

      
    }
}
