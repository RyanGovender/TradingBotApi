using System.Data;
using DbType = TradingBot.ORM.Objects.DbType;

namespace TradingBot.ORM.Connection
{
    internal interface IRelationalConnectionFactory
    {
        IDbConnection GetRelationConnection(DbType dbType = DbType.POSTGRES);
    }
}
