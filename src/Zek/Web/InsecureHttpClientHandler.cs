using System.Net.Http;

namespace Zek.Web
{
    public class InsecureHttpClientHandler : HttpClientHandler
    {
        public InsecureHttpClientHandler()
        {
            // Disable SSL certificate validation
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
        }
    }
}