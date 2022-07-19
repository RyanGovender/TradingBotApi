using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Objects.Exchange
{
    [Table(name:"exchange")]
    public class Symbol
    {
        public Guid ID { get; } = new Guid();
        public string Name { get; init; }
        public string BaseAsset { get; init; }
        public string QuoteAsset { get;init; }
    }
}
