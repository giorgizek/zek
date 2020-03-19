using System;
using Microsoft.AspNetCore.Authorization;

namespace Zek.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeExAttribute : AuthorizeAttribute
    {
        public AuthorizeExAttribute()
        {
        }

        public AuthorizeExAttribute(params string[] roles)
        {
            if (roles != null)
                Roles = string.Join(",", roles);
        }

    }
}
