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


        private IEnumerable<string> _roles;
        public IEnumerable<string> GetRoles()
        {
            return _roles ??= ContextAccessor.HttpContext.User.GetRoles();
        }

        protected IEnumerable<string> FindAll(string type) => ContextAccessor.HttpContext.User.FindAll(type).Select(i => i.Value);

        protected string FindFirstValue(string type) => ContextAccessor.HttpContext.User.FindFirstValue(type);
    }
}
