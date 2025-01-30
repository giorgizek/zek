using Microsoft.Extensions.Options;
using Zek.Contracts;
using Zek.Options;
using Zek.Services.Abstractions;

namespace Zek.Services
{
    public class GeocellSmsSender : BaseSmsSender, ISmsSender
    {
        public GeocellSmsSender()
        {
        }
        //public GeocellSmsSender(SmsSenderOptions options)
        //{
        //    _options = options;
        //}
        public GeocellSmsSender(IOptions<SmsSenderOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }


        private readonly SmsSenderOptions _options;

        //public string GetUrl(string url, string number, string message, string merchantId)
        //{
        //    return $"{url}?src={merchantId}&dst={ParseMobile(number)}&txt={message}";
        //}

        public async Task<IApiResponse> SendAsync(string number, string message) => await SendAsync(_options.Url, number, message, _options.MerchantId);

        public async Task<IApiResponse<string>> SendAsync(string? url, string number, string message, string? merchantId)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException($"{nameof(url)} is required");

            if (string.IsNullOrEmpty(number))
                throw new ArgumentException($"{nameof(number)} is required");

            if (string.IsNullOrEmpty(message))
                throw new ArgumentException($"{nameof(message)} is required");

            if (string.IsNullOrEmpty(merchantId))
                throw new ArgumentException($"{nameof(merchantId)} is required");


            //var url = GetUrl(url, number, message, merchantId);
            var result = new ApiResponse<string>();
            using var httpClient = new HttpClient();
            //result.Content = await httpClient.GetStringAsync(url);

            var content = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("src", merchantId),
                new KeyValuePair<string, string>("dst", ParseMobile(number)),
                new KeyValuePair<string, string>("txt", message)
            ]);

            var response = await httpClient.PostAsync(url, content);
            result.Value = await response.Content.ReadAsStringAsync();

            result.Success = "y".Equals(result.Value, StringComparison.CurrentCultureIgnoreCase);
            return result;
        }

    }
}
