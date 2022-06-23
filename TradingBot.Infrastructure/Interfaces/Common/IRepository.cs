namespace TradingBot.Infrastructure.Interfaces.Common
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> InsertAsync(T data);
        Task<T> UpdateAsync(T data);
        Task<bool> DeleteAsync(T entityToDelete);
        Task<T> FindAsync(object id);
    }
}
