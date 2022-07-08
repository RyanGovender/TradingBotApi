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
        public string? Message { get; init; }
        public T? Source { get; init; }
        public Exception? Exception { get; init; }
        public bool IsSuccess => Exception == null;

        public MatterDapterResponse()
        {

        }

        public MatterDapterResponse(object id, string? message = null)
        {
            Id = id;
            Message = message;
        }


        public MatterDapterResponse(T data)
        {
            Source = data;
        }

        public MatterDapterResponse(T data, Exception exception)
        {
            Message = exception.Message;
            Source = data;
            Exception = exception;
        }

        public MatterDapterResponse(Exception exception)
        {
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
