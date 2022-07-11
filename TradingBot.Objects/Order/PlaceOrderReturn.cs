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
        public decimal? AverageFillPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal Price { get; set; }
        public decimal RequestedQuantity { get; set; }
        public decimal QuantityFilled { get; set; }
        public decimal? StopPrice { get; set; }
        public bool IsOrderFilled => RequestedQuantity == QuantityFilled;
        public Error? Error { get; set; }
        public bool IsSuccess => Error == null;

        public PlaceOrderReturn(long id, int StatusId, decimal? averageFillPrice, DateTime orderTime, decimal price,
            decimal requestedQuantity, decimal quantityFilled, decimal? stopPrice)
        {
            Id = id;
            Status = (Status)StatusId;
            AverageFillPrice = averageFillPrice;
            OrderTime = orderTime;
            Price = price;
            RequestedQuantity = requestedQuantity;
            QuantityFilled = quantityFilled;
            StopPrice = stopPrice;
        }

        public PlaceOrderReturn(string message, int? code, object data)
        {
            Error = new Error(message, code, data);
        }
    }
}
