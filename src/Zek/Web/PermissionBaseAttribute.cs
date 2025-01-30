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

        public PermissionBaseAttribute(int[] permissions, int? action)
            : this()
        {
            Permissions = permissions;
            Action = action;
        }


        private static readonly string[] EmptyArray = [];

        /// <summary>
        /// List of claim types in token
        /// </summary>
        private string[] _claimTypes = EmptyArray;


        private int[] _permissions = [];
        public int[] Permissions
        {
            get => _permissions;
            set
            {
                _permissions = value;
                _claimTypes = SplitString(value);
            }
        }

        public int? Action { get; set; }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            //this validation need if user not authenticated or [AllowAnonymous] the base Authorize filter will response unauthorized;
            if (user.Identity == null || !user.Identity.IsAuthenticated)
                return;

            if (_claimTypes.Length > 0)
            {
                //Claim claim = null;

                if (Action != null)
                {
                    //var hasPermission = false;
                    foreach (var claimType in _claimTypes)
                    {
                        var claim = user.FindFirst(claimType);
                        if (claim == null) continue;

                        if (int.TryParse(claim.Value, out var permission) &&
                            BitwiseHelper.HasFlag(permission, Action.Value))
                        {
                            return;//return means has permission
                            //hasPermission = true;
                            //break;
                        }
                    }

                    //if (!hasPermission)
                    //    context.Result = new ForbidResult();
                }
                else
                {
                    foreach (var claimType in _claimTypes)
                    {
                        var claim = user.FindFirst(claimType);
                        if (claim != null)
                        {
                            return;//return means has permission
                            //break;
                        }
                    }

                    //if (claim == null)
                    //    context.Result = new ForbidResult();
                }

                context.Result = new ForbidResult();
            }
        }


        public static string[] SplitString(int[] permissions)
        {
            if (permissions == null || permissions.Length == 0)
            {
                return EmptyArray;
            }

            var split = from piece in permissions
                            //where piece != 0
                        select CustomClaimTypes.Permission + "_" + piece;
            return split.ToArray();
        }
    }
}