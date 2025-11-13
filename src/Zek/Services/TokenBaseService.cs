using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Zek.Extensions;
using Zek.Extensions.Security.Claims;
using Zek.Services.Abstractions;

namespace Zek.Services
{
    public class TokenBaseService : ITokenBaseService
    {
        protected readonly IHttpContextAccessor _contextAccessor;

        public TokenBaseService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        private int? _userId;
        public virtual int GetUserId()
        {
            _userId ??= _contextAccessor.HttpContext.User.GetUserId().ToInt32();
            return _userId.Value;
        }

        private string? _userName;
        public virtual string? GetUserName()
        {
            _userName ??= _contextAccessor.HttpContext.User.GetUserName();
            return _userName;
        }


        private DateTime? _expirationTime;
        public virtual DateTime GetExpirationTime()
        {
            //if (_expirationTime == null)
            //{
            //    _expirationTime = ContextAccessor.HttpContext.User.GetExpirationTime().GetValueOrDefault(EpochTime.UnixEpoch);
            //}
            _expirationTime ??= (_contextAccessor.HttpContext.User.GetExpirationTime().GetValueOrDefault(EpochTime.UnixEpoch));
            return _expirationTime.Value;
        }


        private IEnumerable<string>? _roles;
        public IEnumerable<string> GetRoles()
        {
            return _roles ??= _contextAccessor.HttpContext.User.GetRoles();
        }

        protected IEnumerable<string> FindAll(string type) => _contextAccessor.HttpContext.User.FindAll(type).Select(i => i.Value);

        protected string? FindFirstValue(string type) => _contextAccessor.HttpContext.User.FindFirstValue(type);


        /// <summary>
        /// Retrieves the client's IP address, prioritizing Cloudflare and standard proxy headers.
        /// </summary>
        /// <remarks>
        /// The resolution order is:
        /// <list type="number">
        /// <item><description><b>CF-Connecting-IP</b>: Primary check for Cloudflare environments.</description></item>
        /// <item><description><b>X-Forwarded-For</b>: Secondary check for standard proxies (returns the first IP in the chain).</description></item>
        /// <item><description><b>RemoteIpAddress</b>: Fallback for direct connections or local development.</description></item>
        /// </list>
        /// </remarks>
        /// <returns>The IP address as a string, or <c>null</c> if the HttpContext is unavailable.</returns>
        public virtual string? GetIpAddress()
        {
            var context = _contextAccessor.HttpContext;
            if (context is null) return null;

            // 1. Priority: Cloudflare Header (True Client IP)
            string? cfIp = context.Request.Headers["CF-Connecting-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(cfIp))
            {
                return cfIp;
            }

            // 2. Secondary: X-Forwarded-For (Standard Proxy Chain)
            string? forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedHeader))
            {
                // The header can be "client, proxy1, proxy2". We want the first one.
                return forwardedHeader.Split(',', StringSplitOptions.RemoveEmptyEntries)[0].Trim();
            }

            // 3. Fallback: Direct Connection IP (Localhost or direct access)
            return _contextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }
    }
}
