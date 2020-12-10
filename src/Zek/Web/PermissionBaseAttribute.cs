using System;
using System.Linq;
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
        //public PermissionBaseAttribute(int? permission, int? action)
        //    : this()
        //{
        //    Permission = permission;
        //    Action = action;
        //}
        public PermissionBaseAttribute(int[] permissions, int? action)
            : this()
        {
            Permissions = permissions;
            Action = action;
        }


        private static readonly string[] EmptyArray = new string[0];

        /// <summary>
        /// List of claim types in token
        /// </summary>
        private string[] _claimTypes = EmptyArray;

        //private int? _permission;
        //public int? Permission
        //{
        //    get => _permission;
        //    set
        //    {
        //        _permission = value;
        //        Permissions = value != null ? new[] { (int)value } : null;
        //    }
        //}


        private int[] _permissions;
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
                Claim claim = null;

                if (Action != null)
                {
                    var hasPermission = false;
                    foreach (var claimType in _claimTypes)
                    {
                        claim = user.FindFirst(claimType);
                        if (claim != null)
                        {
                            if (!string.IsNullOrEmpty(claim.Value) &&
                                int.TryParse(claim.Value, out var permission) &&
                                BitwiseHelper.HasFlag(permission, Action.Value))
                            {
                                hasPermission = true;
                                break;
                            }
                        }
                    }

                    if (!hasPermission)
                        context.Result = new ForbidResult();
                }
                else
                {
                    foreach (var claimType in _claimTypes)
                    {
                        claim = user.FindFirst(claimType);
                        if (claim != null)
                        {
                            break;
                        }
                    }

                    if (claim == null)
                    {
                        context.Result = new ForbidResult();
                    }
                }
            }

            //if (Permission != null)
            //{
            //    var claim = user.FindFirst(_claimType);

            //    if (claim == null)
            //    {
            //        context.Result = new ForbidResult();
            //    }
            //    else if (Action != null)
            //    {
            //        if (string.IsNullOrEmpty(claim.Value) || !int.TryParse(claim.Value, out var permission) || !BitwiseHelper.HasFlag(permission, Permission.Value))
            //        {
            //            context.Result = new ForbidResult();
            //        }
            //    }
            //}
        }


        internal static string[] SplitString(int[] permissions)
        {
            if (permissions == null || permissions.Length == 0)
            {
                return EmptyArray;
            }

            var split = from piece in permissions
                            //where piece != 0
                        select CustomClaimTypes.Permission + "_" + piece;
            ;
            return split.ToArray();
        }
    }
}