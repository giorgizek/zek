using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Zek.Web
{
    public interface IApiKeyService : IDisposable
    {
        public Task<ClaimsPrincipal> ValidateTokenAsync(string key);
    }

    public class ApiKeyAttribute : IAsyncAuthorizationFilter
    {
        private readonly IApiKeyService _apiKeyService;
        private const string AUTH_HEADER_NAME = "X-API-Key";
        private const string API_KEY_QUERY_PARAM_NAME = "api_key";

        public ApiKeyAttribute(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AUTH_HEADER_NAME, out var authHeader)
                && !context.HttpContext.Request.Query.TryGetValue(API_KEY_QUERY_PARAM_NAME, out authHeader)
                )
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (string.IsNullOrEmpty(authHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var principal = await _apiKeyService.ValidateTokenAsync(authHeader);
                context.HttpContext.User = principal;
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
