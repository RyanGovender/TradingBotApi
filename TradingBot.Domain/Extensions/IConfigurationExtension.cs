using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Domain.Extensions
{
    internal static class IConfigurationExtension
    {
        public static string GetBinanceKey(this IConfiguration configuration, string keyName)
        {
            return configuration.GetAppsetting($"Binance:{keyName}");
        }

        public static string GetAppsetting(this IConfiguration configuration, string sectionName)
        {
            var configValue = configuration.GetSection(sectionName);

            if (configValue is null || !configValue.Exists()) throw new KeyNotFoundException();

            return configValue.Value;
        }
    }
}
