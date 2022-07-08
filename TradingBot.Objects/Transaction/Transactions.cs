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
        public int TransactionTypeID { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal OpeningBalance { get; private set; }
        public DateTime TransactionDate { get; private set; } = DateTime.UtcNow;
        public Guid UserID { get; set; }
        public Guid ExchangeID { get; set;}
        public decimal Quantity { get; set; }

        public Transactions()
        {

        }

        public Transactions(TransactionType transactionType, decimal transactionAmount, Guid userID, Guid exchangeID, decimal quantity)
        {
            TransactionTypeID = (int)transactionType; //? value : throw new Exception("Transaction Type not found");
            TransactionAmount = transactionAmount;
            OpeningBalance = 0.0m;
            UserID = userID;
            ExchangeID = exchangeID;
            Quantity = quantity;
        }
    }
}
