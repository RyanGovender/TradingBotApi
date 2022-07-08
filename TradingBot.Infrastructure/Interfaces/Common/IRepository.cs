using TradingBot.Objects.Enums;

namespace TradingBot.Infrastructure.Interfaces.Common
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<Result> InsertAsync(T data);
        Task<Result> UpdateAsync(T data);
        Task<Result> DeleteAsync(T entityToDelete);
        Task<T> FindAsync(object id);
    }
}
