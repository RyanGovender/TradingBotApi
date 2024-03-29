﻿using System.Threading.Tasks;
using TradingBot.ORM.Objects;


namespace TradingBot.ORM.Interfaces
{
    public interface IBaseRepository
    {
        Task<MatterDapterResponse<IEnumerable<T>>> GetAllAsync<T>() where T : class;
        Task<MatterDapterResponse<T>> InsertAsync<T>(T data) where T : class;
        Task<MatterDapterResponse<T>> UpdateAsync<T>(T data) where T : class;
        Task<MatterDapterResponse> DeleteAsync<T>(T entityToDelete) where T : class;
        Task<MatterDapterResponse<T>> FindAsync<T>(object id) where T : class;
        Task<MatterDapterResponse<IEnumerable<T>>> RunQueryAsync<T>(string? storeProcedure = null, string? sqlStatement = null, object? parameters = null);
        Task<MatterDapterResponse<T>> RunQuerySingleAsync<T>(string? storeProcedure = null, string? sqlStatement = null, object? parameters = null);
    }
}
