using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Infrastructure.Infrastruture.Common
{
    internal static class Function
    {
        private static string SchemaName => "exchange.";
        public static string GetBotOrderTransactionUsingBinanceID => CreateFunctionName("getbotordertransactionformbinanceid");
        public static string GetBotOrderTransactionUsingID=> CreateFunctionName("getbotordertransactionformid");
        public static string GetLastestTransactonUsingBotOrderID => CreateFunctionName("getlatesttransactionusingbotorderid");
        public static string HasBotOrderTransaction => CreateFunctionName("hasbotordertransactions");
        private static string CreateFunctionName(string functionName, string? schemaName = null)
        {
            schemaName ??= SchemaName;
            return schemaName + functionName;
        }
    }
}
