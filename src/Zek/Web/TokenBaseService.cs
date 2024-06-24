using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Zek.Extensions;
using Zek.Extensions.Security.Claims;

namespace Zek.Web
{
    public interface ITokenBaseService
    {
        int GetUserId();
        string GetUserName();
        DateTime GetExpirationTime();
        IEnumerable<string> GetRoles();
        string GetIpAddress();
    }

    public class TokenBaseService : ITokenBaseService
    {
        protected readonly IHttpContextAccessor ContextAccessor;

        public TokenBaseService(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
        }


        private int? _userId;
        public virtual int GetUserId()
        {
            _userId ??= ContextAccessor.HttpContext.User.GetUserId().ToInt32();
            return _userId.Value;
        }

        private string _userName;
        public virtual string GetUserName()
        {
            _userName ??= ContextAccessor.HttpContext.User.GetUserName();
            return _userName;
        }


        private DateTime? _expirationTime;
        public virtual DateTime GetExpirationTime()
        {
            //if (_expirationTime == null)
            //{
            //    _expirationTime = ContextAccessor.HttpContext.User.GetExpirationTime().GetValueOrDefault(EpochTime.UnixEpoch);
            //}
            _expirationTime ??= (ContextAccessor.HttpContext.User.GetExpirationTime().GetValueOrDefault(EpochTime.UnixEpoch));
            return _expirationTime.Value;
        }


        private IEnumerable<string> _roles;
        public IEnumerable<string> GetRoles()
        {
            return _roles ??= ContextAccessor.HttpContext.User.GetRoles();
        }

        protected IEnumerable<string> FindAll(string type) => ContextAccessor.HttpContext.User.FindAll(type).Select(i => i.Value);

        protected string FindFirstValue(string type) => ContextAccessor.HttpContext.User.FindFirstValue(type);


        public virtual string GetIpAddress()
        {
            return ContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }
    }
}
