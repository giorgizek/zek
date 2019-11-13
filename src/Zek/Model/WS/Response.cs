using System;

namespace Zek.Model.WS
{
    public class Response
    {
        public Response(string requestId = null, int errorCode = 0, string errorMessage = null)
        {
            Timestamp = DateTime.Now;
            RequestId = requestId;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public string RequestId { get; set; }
        public DateTime Timestamp { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Exception { get; set; }

        public string LogId { get; set; }
    }

    public class Response<T> : Response
    {
        public Response(string requestId = null, int errorCode = 0, string errorMessage = null) : base(requestId, errorCode, errorMessage)
        {
            
        }

        public T Value { get; set; }

    }
}
