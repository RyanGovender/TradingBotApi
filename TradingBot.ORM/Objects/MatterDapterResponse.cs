using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.ORM.Objects
{
    public class MatterDapterResponse<T>
    {
        public object? Id { get; set; }
        public bool IsSuccess { get; init; }
        public string? Message { get; init; }
        public T? Source { get; init; }
        public Exception? Exception { get; init; }

        public MatterDapterResponse()
        {

        }

        public MatterDapterResponse(object id, bool isSucess = true, string? message = null)
        {
            Id = id;
            IsSuccess = isSucess;
            Message = message;
        }

        public MatterDapterResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }


        public MatterDapterResponse(T data, bool sucess = true)
        {
            IsSuccess = sucess;
            Source = data;
        }

        public MatterDapterResponse(T data, Exception exception)
        {
            IsSuccess = false;
            Message = exception.Message;
            Source = data;
            Exception = exception;
        }

        public MatterDapterResponse(Exception exception)
        {
            IsSuccess = false;
            Message = exception.Message;
            Exception = exception;
        }

    }

    public class MatterDapterResponse
    {
        public bool IsSuccess { get; init; }
        public string? Message { get; init; }
        public Exception? Exception { get; init; }

        public MatterDapterResponse(bool result)
        {
            IsSuccess = result;
        }

        public MatterDapterResponse(Exception ex)
        {
            IsSuccess = false;
            Message = ex.Message;
            Exception = ex;
        }
    }
}
