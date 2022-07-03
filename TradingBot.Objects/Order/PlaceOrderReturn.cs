using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Objects.Common;
using TradingBot.Objects.Enums;

namespace TradingBot.Objects.Order
{
    public class PlaceOrderReturn
    {
        public long Id { get; set; }
        public Status Status { get; set; }
        public decimal AverageFillPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal Price { get; set; }
        public decimal RequestedQuantity { get; set; }
        public decimal QuantityFilled { get; set; }
        public decimal? StopPrice { get; set; }
        public Error? Error { get; set; }
        public bool IsSuccess => Error == null;

        public PlaceOrderReturn()
        {

        }

        public PlaceOrderReturn(long id)
        {

        }
    }
}
