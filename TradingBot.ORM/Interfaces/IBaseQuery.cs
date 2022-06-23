using TradingBot.ORM.Objects;

namespace TradingBot.ORM.Interfaces
{
    public interface IBaseQuery
    {
        Task<MatterDapterResponse<IEnumerable<T>>> RunQueryAsync<T>(string? storeProcedure = null, string? sqlStatement = null, object? parameters = null);
    }
}
