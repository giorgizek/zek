using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Zek.Model.Config;
using Zek.Model.DTO.Google;
using Zek.Utils;

namespace Zek.Services
{
    public interface IRecaptchaService : IDisposable
    {
        string GetSiteKey();
        Task<ReCaptchaVerifyResponseDTO> VerifyAsync(string token, string ip = null);
    }
    public class RecaptchaService : DisposableObject, IRecaptchaService
    {
        private readonly reCAPTCHAOptions _options;

        public RecaptchaService(IOptions<reCAPTCHAOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }
        protected override void DisposeResources()
        {

        }

        public string GetSiteKey()
        {
            return _options.SiteKey;
        }

        public async Task<ReCaptchaVerifyResponseDTO> VerifyAsync(string token, string ip = null)
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _options.SecretKey),
                new KeyValuePair<string, string>("response", token)
            });

            var response = await client.PostAsync(_options.VerifyingUrl, content);
            //var json = await response.Content.ReadAsStringAsync();
            var result = await response.Content.ReadFromJsonAsync<ReCaptchaVerifyResponseDTO>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true,
            });

            return result;
        }
    }
}
