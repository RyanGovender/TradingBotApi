using Microsoft.Extensions.Configuration;
using System.Data;
using DbType = TradingBot.ORM.Objects.DbType;
using Npgsql;

namespace TradingBot.ORM.Connection
{
    internal class RelationalConnectionFactory : IRelationalConnectionFactory
    {
        private readonly IConfiguration _config;
        public RelationalConnectionFactory(IConfiguration configuration)
        {
            _config = configuration ?? throw new Exception("Error with configuaration.");
        }

        public IDbConnection GetRelationConnection(DbType dbType = DbType.POSTGRES)
        {
            return dbType switch
            {
                DbType.POSTGRES => new NpgsqlConnection(_config.GetConnectionString("Postgres")),
                _ => throw new NotSupportedException($"Connection could not be created for {dbType}.")
            };
        }
    }
}
