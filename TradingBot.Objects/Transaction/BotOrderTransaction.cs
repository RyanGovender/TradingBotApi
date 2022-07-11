using System.ComponentModel.DataAnnotations.Schema;

namespace TradingBot.Objects.Transaction
{
    [Table(name:"botordertransactions")]
    public class BotOrderTransactions
    {
        public Guid BotOrderID { get; init; }
        public Guid TransactionID { get; init; }
        public long BinanceOrderID { get; init; }
        public bool IsOrderFilled { get; set; }
    }
}
