namespace TradingBot.Objects.Common
{
    public class Error
    {
        public string Message { get; set; }
        public int? Code { get; set; }
        public object Data { get;set; }

        public Error(string message, int? code, object data)
        {
            Message = message;
            Code = code;
            Data = data;
        }
    }
}
