using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Objects.Transaction
{
    public record LatestTransaction(long BinanceOrderID, decimal TransactionAmount, int TransactionTypeID, decimal Quantity, bool IsOrderFilled);
}
