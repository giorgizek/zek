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


        public virtual string? GetIpAddress()
        {
            return _contextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }
    }
}
