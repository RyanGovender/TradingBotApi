using Dapper.Contrib.Extensions;
using System.Data;
using TradingBot.ORM.Connection;
using TradingBot.ORM.Interfaces;
using TradingBot.ORM.Objects;

namespace TradingBot.ORM.Base
{
    internal class Base : IBaseRepository
    {
        private readonly IRelationalConnectionFactory _conn;

        public Base(IRelationalConnectionFactory connection)
        {
            _conn = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task<MatterDapterResponse> DeleteAsync<T>(T entityToDelete) where T : class
        {
            try
            {
                using IDbConnection connection = _conn.GetRelationConnection();

                var result = await connection.DeleteAsync(entityToDelete).ConfigureAwait(false);

                return new MatterDapterResponse(result);
            }
            catch (Exception ex)
            {
                return new MatterDapterResponse(ex);
            }
        }

        public async Task<MatterDapterResponse<T>> FindAsync<T>(object id) where T : class
        {
            try
            {
                using IDbConnection connection = _conn.GetRelationConnection();

                if (id is null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await connection.GetAsync<T>(id);

                return new MatterDapterResponse<T>(result);
            }
            catch (Exception ex)
            {
                return new MatterDapterResponse<T>(ex);
            }
        }

        public async Task<MatterDapterResponse<IEnumerable<T>>> GetAllAsync<T>() where T : class
        {
            try
            {
                using IDbConnection connection = _conn.GetRelationConnection();

                var result = await connection.GetAllAsync<T>();

                return new MatterDapterResponse<IEnumerable<T>>(result);
            }
            catch (Exception ex)
            {
                return new MatterDapterResponse<IEnumerable<T>>(new List<T>(), ex);
            }
        }

        public async Task<MatterDapterResponse<T>> InsertAsync<T>(T data) where T : class
        {
            try
            {
                using IDbConnection connection = _conn.GetRelationConnection();

                var result = await connection.InsertAsync(data).ConfigureAwait(false);

                if (result == 0)
                {
                    return new MatterDapterResponse<T>(false);
                }

                return new MatterDapterResponse<T>(result, true, "Success");

            }
            catch (Exception ex)
            {
                return new MatterDapterResponse<T>(ex);
            }
        }

        public async Task<MatterDapterResponse<T>> UpdateAsync<T>(T data) where T : class
        {
            try
            {
                using IDbConnection connection = _conn.GetRelationConnection();

                var result = await connection.UpdateAsync(data).ConfigureAwait(false);

                return new MatterDapterResponse<T>(result);
            }
            catch (Exception ex)
            {
                return new MatterDapterResponse<T>(ex);
            }
        }
    }
}
