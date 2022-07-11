namespace TradingBot.Objects.Enums
{
    public enum Trade
        {
            BUY = 0,
            SELL = 1,
            HOLD = 2,
            UNKNOWN = 3
        }

    public enum Status {
        New,
        /// <summary>
        /// Order is partly filled, still has quantity left to fill
        /// </summary>
        PartiallyFilled,
        /// <summary>
        /// The order has been filled and completed
        /// </summary>
        Filled,
        /// <summary>
        /// The order has been canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// The order is in the process of being canceled  (currently unused)
        /// </summary>
        PendingCancel,
        /// <summary>
        /// The order has been rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// The order has expired
        /// </summary>
        Expired,
        /// <summary>
        /// Liquidation with Insurance Fund
        /// </summary>
        Insurance,
        /// <summary>
        /// Counterparty Liquidation
        /// </summary>
        Adl
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

    public enum Result
    {
        SUCCESSFUL = 0,
        FAILED = 1,
        ERROR = 2
    }

    public static class EnumExtension
    {
        public static int GetIntValue<TEnum>(this TEnum enums) where TEnum : struct, Enum
        {
            int result = (int)enums.GetTypeCode();

            return result;
        }

        public static bool GetIntValue<TEnum>(this TEnum enums, out int value) where TEnum : struct
        {
            var isValueExistingEnum = Enum.TryParse(enums.ToString(), out int result);

            value = result;

            return isValueExistingEnum;
        }
    }
}
