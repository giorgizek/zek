using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zek.Security.Claims;
using Zek.Utils;

namespace Zek.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PermissionBaseAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public PermissionBaseAttribute()
        {

        }
        public PermissionBaseAttribute(int permission, int action)
            : this()
        {
            Permission = permission;
            Action = action;
        }


        public int Permission { get; set; }
        public int Action { get; set; }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                return;
            }



            //bool isAuthorized = MumboJumboFunction(context.HttpContext.User, _item, _action); // :)

            //if (!isAuthorized)
            //{
            //    context.Result = new ForbidResult();
            //}

            var key = CustomClaimTypes.Permission + "_" + Permission;
            var permissionValue = user.FindFirstValue(key);
            if (string.IsNullOrEmpty(permissionValue) || !int.TryParse(permissionValue, out var permission) || BitwiseHelper.HasFlag(permission, Permission))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}