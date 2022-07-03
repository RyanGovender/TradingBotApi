using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Objects.Enums;

namespace TradingBot.Objects.Order
{
    public class PlaceOrderData
    {
        [DisallowNull]
        public string CurrencySymbol { get; set; }
        public int OrderSideId { get; set; }
        public int OrderTypeId { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? StopPrice { get; set; }
    }
}
