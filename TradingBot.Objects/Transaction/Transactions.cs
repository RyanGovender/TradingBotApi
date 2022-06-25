using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Objects.Transaction
{
    [Table(name:"transaction")]
    public class Transactions
    {
        [Column(name:"id")]
        public Guid ID { get; set; } = Guid.NewGuid();
        public int TransactionTypeID { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal OpeningBalance { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public Guid UserID { get; set; }
        public Guid ExchangeID { get; set;}
    }
}
