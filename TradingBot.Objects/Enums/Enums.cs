using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Objects.Enums
{
        public enum Trade
        {
            SELL = 0,
            BUY = 1,
            HOLD = 2,
            UNKNOWN = 3
        }
        
        public enum TransactionType
        {
            SELL = 1,
            BUY = 2
        }

        public enum TradeStrategy
        {
            SIMPLE_TRADE = 1
        }

    public static class EnumExtension
    {
        public static int GetIntValue<TEnum>(this TEnum enums) where TEnum : struct
        {
            var isValueExistingEnum = Enum.TryParse(enums.ToString(), out int result);

            return isValueExistingEnum ? result : default;
        }

        public static bool GetIntValue<TEnum>(this TEnum enums, out int value) where TEnum : struct
        {
            var isValueExistingEnum = Enum.TryParse(enums.ToString(), out int result);

            value = result;

            return isValueExistingEnum;
        }
    }
}
