using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Zek.Web
{
    [Flags]
    public enum ApiKeyStatus
    {
        Valid = 0,
        MissingKey = 1,
        InvalidKey = 2,
    }

    public interface IApiKeyService : IDisposable
    {
        public ApiKeyStatus IsValidAsync(string key);
    }

    public class ApiKeyAttribute : IAsyncAuthorizationFilter
    {
        private readonly IApiKeyService _apiKeyService;
        private const string ApiKeyHeaderName = "X-API-Key";
        private const string ApiKeyQueryParamName = "api_key";

        public ApiKeyAttribute(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var key)
                && !context.HttpContext.Request.Query.TryGetValue(ApiKeyQueryParamName, out key)
                )
            {
                context.Result = new UnauthorizedObjectResult(nameof(ApiKeyStatus.MissingKey));
                return;
            }

            if (string.IsNullOrEmpty(key))
            {
                context.Result = new UnauthorizedObjectResult(nameof(ApiKeyStatus.MissingKey));
                return;
            }



            //var status = await _apiKeyService.IsValidAsync(key);

            await Task.CompletedTask;
        }
    }
}
