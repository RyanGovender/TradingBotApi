using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Objects.Enums;

namespace TradingBot.Objects.Transaction
{
    [Table(name:"transaction")]
    public class Transactions
    {
        public Guid TransactionID { get; private set; } = Guid.NewGuid();
        public int TransactionTypeID { get; private set; }
        public decimal TransactionAmount { get; set; }
        public decimal OpeningBalance { get; private set; }
        public DateTime TransactionDate { get; private set; } = DateTime.UtcNow;
        public Guid UserID { get; set; }
        public Guid ExchangeID { get; set;}

        public Transactions(TransactionType transactionType, decimal transactionAmount, decimal openingBalance, DateTime transactionDate, Guid userID, Guid exchangeID)
        {
            TransactionTypeID = transactionType.GetIntValue(out int value) ? value : throw new Exception("Transaction Type not found");
            TransactionAmount = transactionAmount;
            OpeningBalance = openingBalance;
            TransactionDate = transactionDate;
            UserID = userID;
            ExchangeID = exchangeID;
        }
    }
}
