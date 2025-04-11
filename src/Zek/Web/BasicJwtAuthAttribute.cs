using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Zek.Options;
using Zek.Security.Claims;

namespace Zek.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class BasicJwtAuthAttribute : PermissionBaseAttribute
    {
        private const string AUTH_HEADER_NAME = "Authorization";
        private const string AUTH_PREFIX = "Basic ";

        public BasicJwtAuthAttribute()
        {
        }

        public BasicJwtAuthAttribute(int[] permissions, int? action)
            : this()
        {
            Permissions = permissions;
            Action = action;
        }



        public override void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AUTH_HEADER_NAME, out var auth))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (string.IsNullOrEmpty(auth))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var authHeader = auth.ToString();
            if (!authHeader.StartsWith(AUTH_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var token = authHeader.Substring(AUTH_PREFIX.Length).Trim();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = !string.IsNullOrEmpty(ConfigBase.TokenOptions.ValidIssuer),
                    ValidIssuer = ConfigBase.TokenOptions.ValidIssuer,

                    ValidateAudience = !string.IsNullOrEmpty(ConfigBase.TokenOptions.ValidAudience),
                    ValidAudience = ConfigBase.TokenOptions.ValidAudience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = !string.IsNullOrEmpty(ConfigBase.TokenOptions.IssuerSigningKey),
                    IssuerSigningKey = !string.IsNullOrEmpty(ConfigBase.TokenOptions.IssuerSigningKey)
                       ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigBase.TokenOptions.IssuerSigningKey))
                       : null,
                    NameClaimType = CustomClaimTypes.UserName,
                    RoleClaimType = CustomClaimTypes.Role,
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                context.HttpContext.User = principal;
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            base.OnAuthorization(context);
        }
    }
}
