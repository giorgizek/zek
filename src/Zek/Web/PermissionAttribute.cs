using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
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
        public PermissionBaseAttribute(int? permission, int? action)
            : this()
        {
            Permission = permission;
            Action = action;
        }


        public int? Permission { get; set; }
        public int? Action { get; set; }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            //this validation need if user not authenticated or [AllowAnonymous] the base Authorize filter will response unauthorized;
            if (!user.Identity.IsAuthenticated)
                return;

            if (Permission != null)
            {
                var claimType = CustomClaimTypes.Permission + "_" + Permission;
                var claim = user.FindFirst(claimType);

                if (Action != null)
                {
                    var permissionValue = user.FindFirstValue(claimType);
                    if (string.IsNullOrEmpty(permissionValue) || !int.TryParse(permissionValue, out var permission) || BitwiseHelper.HasFlag(permission, Permission.Value))
                    {
                        context.Result = new ForbidResult();
                    }
                }
                else if (!user.HasClaim(c => c.Type == claimType))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}