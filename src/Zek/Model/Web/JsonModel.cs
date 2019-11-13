using System;

namespace Zek.Model.Web
{
    public class JsonModel
    {
        public JsonModel()
        {
        }

        public JsonModel(string message) : this(null, message) { }
        public JsonModel(Enum error) : this(Convert.ToInt32(error), error.ToString()) { }
        public JsonModel(int? statusCode = null, string message = null)
        {
            StatusCode = statusCode;
            Message = message;
        }

        //public bool IsValid => ErrorCode == 0;

        public int? StatusCode { get; set; }

        public string Message { get; set; }
    }

    public class JsonModel<T> : JsonModel
    {
        public JsonModel()
        {
        }
        public JsonModel(T value, string message) : this(value, null, message) { }
        public JsonModel(T value, Enum error) : this(value, Convert.ToInt32(error), error.ToString()) { }
        public JsonModel(T value, int? statusCode = null, string message = null)
        {
            Value = value;
            StatusCode = statusCode;
            Message = message;
        }

        public T Value { get; set; }

    }
}
