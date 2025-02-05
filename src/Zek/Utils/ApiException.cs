using System.Net;

namespace Zek.Utils
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public string? Content { get; set; }
    }
}
