using System.ComponentModel.DataAnnotations.Schema;

namespace TradingBot.Objects.Transaction
{
    [Table(name:"botordertransactions")]
    public class BotOrderTransactions
    {
        public Guid BotOrderID { get; set; }
        public Guid TransactionID { get; set; }
    }
}
