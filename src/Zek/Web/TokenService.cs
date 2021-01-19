using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Zek.Extensions;
using Zek.Extensions.Security.Claims;

namespace Zek.Web
{
    public interface ITokenBaseService
    {
        int GetUserId();
        IEnumerable<string> GetRoles();

    }

    public class TokenBaseService : ITokenBaseService
    {
        private readonly IHttpContextAccessor _contextAccessor;

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


        private IEnumerable<string> _roles;
        public IEnumerable<string> GetRoles()
        {
            return _roles ?? (_roles = _contextAccessor.HttpContext.User.GetRoles());
        }

        protected IEnumerable<string> FindAll(string type) => _contextAccessor.HttpContext.User.FindAll(type).Select(i => i.Value);

        protected string FindFirstValue(string type) => _contextAccessor.HttpContext.User.FindFirstValue(type);
    }
}
