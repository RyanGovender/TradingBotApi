using Microsoft.Extensions.DependencyInjection;
using TradingBot.ORM.Connection;
using TradingBot.ORM.Interfaces;

namespace TradingBot.ORM.Extension
{
    public static class IServiceExtension
    {
        public static void AddOrmHelper(this IServiceCollection services)
        {
            services.AddSingleton<IRelationalConnectionFactory, RelationalConnectionFactory>();
            services.AddSingleton<IBaseQuery, Base.Base>();
            services.AddSingleton<IBaseRepository, Base.Base>();
        }
    }
}
