using Microsoft.Extensions.Logging;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Enums;
using TradingBot.ORM.Interfaces;

namespace TradingBot.Infrastructure.Infrastruture.Common
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly IBaseRepository _base;
        private readonly ILogger<T> _logger;

        public BaseRepository(IBaseRepository baseRepository, ILogger<T> logger)
        {
            _base = baseRepository;
            _logger = logger;
        }

        public async Task<Result> DeleteAsync(T entityToDelete)
        {
            var result = await _base.DeleteAsync(entityToDelete);

            if (!result.IsSuccess)
                _logger.LogError(result?.Exception?.Message ?? "Critical Error.", entityToDelete);

            return result?.IsSuccess ?? false ? Result.SUCCESSFUL : Result.FAILED;
        }

        public async Task<T> FindAsync(object id)
        {
            var result = await _base.FindAsync<T>(id);

            if (!result.IsSuccess)
                _logger.LogError(result?.Exception?.Message ?? "Critical Error.", id);

            return result?.Source != null && result.IsSuccess ? result.Source: new();

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _base.GetAllAsync<T>();

            if (!result.IsSuccess)
                _logger.LogError(result?.Exception?.Message ?? "Critical Error.");

            return result?.Source != null && result.IsSuccess ? result.Source : Array.Empty<T>();
        }

        public virtual async Task<Result> InsertAsync(T data)
        {
           var result = await _base.InsertAsync(data);

            if (!result.IsSuccess)
                _logger.LogError(result?.Exception?.Message ?? "Critical Error.", data);

            return result?.IsSuccess ?? false ? Result.SUCCESSFUL : Result.FAILED;
        }

        public async Task<Result> UpdateAsync(T data)
        {
            var result = await _base.UpdateAsync(data);

            if (!result.IsSuccess)
                _logger.LogError(result?.Exception?.Message ?? "Critical Error.", data);

            return result?.IsSuccess ?? false ? Result.SUCCESSFUL : Result.FAILED;
        }
    }
}
