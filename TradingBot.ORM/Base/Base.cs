using Dapper;
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

        public Base(IRelationalConnectionFactory connection!!)
        {
            _conn = connection;
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
                return new MatterDapterResponse<IEnumerable<T>>(Array.Empty<T>(), ex);
            }
        }

        public async Task<MatterDapterResponse<T>> InsertAsync<T>(T data) where T : class
        {
            try
            {
                using IDbConnection connection = _conn.GetRelationConnection();

                var result = await connection.InsertAsync(data).ConfigureAwait(false);

                return new MatterDapterResponse<T>(result, "Success");
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

        //string storeProcedure!!
        public async Task<MatterDapterResponse<IEnumerable<T>>> RunQueryAsync<T>(string? storeProcedure = null, string? sqlStatement = null, object? parameters = null)
        {
            try
            {
                if (string.IsNullOrEmpty(storeProcedure) && string.IsNullOrEmpty(sqlStatement))
                {
                    throw new (nameof(sqlStatement) + nameof(storeProcedure));
                }

                CommandType commandType = storeProcedure != null ? CommandType.StoredProcedure : CommandType.Text;

                using IDbConnection connection = _conn.GetRelationConnection();

                var result = await connection.QueryAsync<T>(storeProcedure ?? sqlStatement , parameters, commandType: commandType);

                return new MatterDapterResponse<IEnumerable<T>>(result);
            }
            catch(Exception ex)
            {
                return new MatterDapterResponse<IEnumerable<T>>(Array.Empty<T>(), ex);
            }
        }

        public async Task<MatterDapterResponse<T>> RunQuerySingleAsync<T>(string? storeProcedure = null, string? sqlStatement = null, object? parameters = null) 
        {
            try
            {
                if (string.IsNullOrEmpty(storeProcedure) && string.IsNullOrEmpty(sqlStatement))
                {
                    throw new (nameof(sqlStatement) + nameof(storeProcedure));
                }

                CommandType commandType = storeProcedure != null ? CommandType.StoredProcedure : CommandType.Text;

                using IDbConnection connection = _conn.GetRelationConnection();

                var result = await connection.QuerySingleAsync<T>(storeProcedure ?? sqlStatement , parameters, commandType: commandType);

                if(result == null) return new MatterDapterResponse<T>();

                return new MatterDapterResponse<T>(result);
            }
            catch(InvalidOperationException ex)
            {
                return new MatterDapterResponse<T>(default, ex);
            }
            catch(Exception ex)
            {
                return new MatterDapterResponse<T>(ex);
            }
        }
    }
}
