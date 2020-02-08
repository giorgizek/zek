/*using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zek.Extensions;
using Zek.Utils;

namespace Zek.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        public ApiAuthorizeAttribute()
        {
        }

        public ApiAuthorizeAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }

        public static IdLinkMode Schema = IdLinkMode.Aes;
        public static string Key = "0d455baa-5c79-4624-85a4-4521f7aa579f";
        public static int DataCount = 4;
        private static readonly string[] EmptyArray = new string[0];

        private string _roles;
        private string[] _rolesSplit = EmptyArray;
        /// <summary>
        /// Gets or sets the authorized roles.
        /// </summary>
        /// <value>
        /// The roles string.
        /// </value>
        /// <remarks>Multiple role names can be specified using the comma character as a separator.</remarks>
        public string Roles
        {
            get => _roles ?? string.Empty;
            set
            {
                _roles = value;
                _rolesSplit = SplitString(value);
            }
        }


        private string _users;
        private string[] _usersSplit = EmptyArray;
        /// <summary>
        /// Gets or sets the authorized users.
        /// </summary>
        /// <value>
        /// The users string.
        /// </value>
        /// <remarks>Multiple role names can be specified using the comma character as a separator.</remarks>
        public string Users
        {
            get { return _users ?? string.Empty; }
            set
            {
                _users = value;
                _usersSplit = SplitString(value);
            }
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authorizationHeader = "";

            if (context.HttpContext.Request.Headers.TryGetValue("AuthorizationHeader", out var authorizationHeaders))
            {
                authorizationHeader = authorizationHeaders.First();
            }

            if (!AuthenticationHeaderValue.TryParse(authorizationHeader, out AuthenticationHeaderValue value))
            {
                context.Result = new ForbidResult();
                return;
            }

            var token = value.Parameter;
            //var scheme = value.Scheme;

            var props = IdLinkHelper.Decode(token, Key, Schema);
            if (props == null || props.Length != DataCount || props[3].ParseUniversalDateTime() < DateTime.Now)
            {
                context.Result = new ForbidResult();
                return;
            }

            if (!IsAuthorized(context, props))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            Bind(context.Controller as ControllerBase, props);
        }

        protected virtual bool IsAuthorized(ActionExecutingContext context, string[] props)
        {
            var userId = props[0].ToInt32();
            if (userId == 0)
            {
                return false;
            }

            var userName = props[1];
            if (_usersSplit.Length > 0 && !_usersSplit.Contains(userName, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            var roles = SplitString(props[2]);
            if (_rolesSplit.Length > 0 && !_rolesSplit.Any(r => roles.Contains(r, StringComparer.OrdinalIgnoreCase)))
            {
                return false;
            }

            return true;
        }

        public virtual void Bind(ControllerBase controller, string[] props)
        {
            var baseController = controller as ApiController;
            if (baseController != null)
                baseController.UserId = props[0].ToInt32();
        }



        /// <summary>
        /// UserId||UserName||Roles||ExpiryDate||A||B||C||
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="roles"></param>
        /// <param name="expiryDate"></param>
        /// <param name="props">parameters</param>
        /// <returns></returns>
        public static string GenerateToken(int userId, string userName, string[] roles, DateTime expiryDate, List<string> props = null)
        {
            if (props == null)
                props = new List<string>();

            props.InsertRange(0, new List<string> { userId.ToString(null, NumberFormatInfo.InvariantInfo), userName, string.Join(",", roles), expiryDate.ToString(DateTimeExtensions.UniversalDateTimeFormat) });

            return GenerateToken(props);
        }

        /// <summary>
        /// UserId||UserName||Roles||ExpiryDate||A||B||C||
        /// </summary>
        /// <param name="props">parameters</param>
        /// <returns>encripted token</returns>
        public static string GenerateToken(IEnumerable<string> props)
        {
            return $"{Schema.ToString()} {IdLinkHelper.Encode(props, Key, Schema)}";
        }


        /// <summary>
        /// Splits the string on commas and removes any leading/trailing whitespace from each result item.
        /// </summary>
        /// <param name="original">The input string.</param>
        /// <returns>An array of strings parsed from the input <paramref name="original"/> string.</returns>
        internal static string[] SplitString(string original)
        {
            if (string.IsNullOrEmpty(original))
            {
                return EmptyArray;
            }

            var split = from piece in original.Split(',')
                        let trimmed = piece.Trim()
                        where !string.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
        }
    }
}
*/