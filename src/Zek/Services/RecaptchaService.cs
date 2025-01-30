using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Zek.Contracts;
using Zek.Options;
using Zek.Services.Abstractions;
using Zek.Utils;

namespace Zek.Services
{
    public class RecaptchaService : DisposableObject, IRecaptchaService
    {
        private readonly ReCaptchaOptions _options;

        public RecaptchaService(IOptions<ReCaptchaOptions> optionsAccessor)
        {
            _options = optionsAccessor?.Value ?? new ReCaptchaOptions();
        }
        protected override void DisposeResources()
        {

        }

        public string GetSiteKey()
        {
            return _options.SiteKey;
        }

        public async Task<ReCaptchaVerifyResponseDto?> VerifyAsync(string token, string? ip = null)
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("secret", _options.SecretKey),
                new KeyValuePair<string, string>("response", token)
            ]);

            var response = await client.PostAsync(_options.VerifyingUrl, content);
            //var json = await response.Content.ReadAsStringAsync();
            var result = await response.Content.ReadFromJsonAsync<ReCaptchaVerifyResponseDto>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            return result;
        }
    }
}
