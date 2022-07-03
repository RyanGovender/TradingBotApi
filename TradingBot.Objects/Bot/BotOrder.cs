using System.ComponentModel.DataAnnotations.Schema;

namespace TradingBot.Objects.Bot
{
    [Table("BotOrder")]
    public class BotOrder
    {
        public Guid ID { get; set; } = new Guid();
        public Guid UserID { get; set; }
        public Guid ExchangeID { get; set; }
        public int OrderTypeID { get; set; }
        public int TradeStrategyID { get; set; }
        public decimal Quantity { get; set; }
        public bool HasTimeFrame { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
