using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Zek.Model.Config;

namespace Zek.Services
{
    public interface ISmsSender
    {
        Task<SendSmsResponse> SendSmsAsync(string number, string message);

        Task<SendSmsResponse> SendSmsAsync(string url, string number, string message, string merchantId);
        //string GetUrl(string url, string number, string message, string merchantId);
    }

    public class SendSmsResponse
    {
        /// <summary>
        /// If message sent then true.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Response from http2sms service (if provider is Geocell then content=Y or N).
        /// </summary>
        public string Content { get; set; }
    }

    public class BaseSmsSender
    {
        public static string ParseMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
                return mobile;

            var result = new string(mobile.ToCharArray().Where(char.IsDigit).ToArray());

            //foreach (var c in mobile)
            //    if (char.IsDigit(c)) result += c;

            if (result.StartsWith("995") && result.Length == 11)
                result = result.Insert(3, "5");
            else if (result.StartsWith("8") && result.Length == 9)
                result = "9955" + result.Substring(1);
            else if (result.StartsWith("5") && result.Length == 9)
                result = "995" + result;
            return result;
        }
    }


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

        public async Task<SendSmsResponse> SendSmsAsync(string number, string message) => await SendSmsAsync(_options.Url, number, message, _options.MerchantId);

        public async Task<SendSmsResponse> SendSmsAsync(string url, string number, string message, string merchantId)
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
            var result = new SendSmsResponse();
            using var httpClient = new HttpClient();
            //result.Content = await httpClient.GetStringAsync(url);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("src", merchantId),
                new KeyValuePair<string, string>("dst", ParseMobile(number)),
                new KeyValuePair<string, string>("txt", message)
            });

            var response = await httpClient.PostAsync(url, content);
            result.Content = await response.Content.ReadAsStringAsync();

            result.Succeeded = "y".Equals(result.Content, StringComparison.CurrentCultureIgnoreCase);
            return result;
        }

    }
}
