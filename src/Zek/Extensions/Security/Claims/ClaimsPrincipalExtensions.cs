using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Reflection;
using Zek.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Zek.Utils;

namespace Zek.Extensions.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static string AuthenticationType { get; set; } = IdentityConstants.ApplicationScheme;

        /// <summary>
        /// Returns the User ID claim value if present otherwise returns null.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>The User ID claim value, or null if the claim is not present.</returns>
        /// <remarks>The User ID claim is identified by <see cref="CustomClaimTypes.UserId"/>.</remarks>
        public static string? GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            return principal.FindFirstValue(CustomClaimTypes.UserId);
        }

        /// <summary>
        /// Returns the Name claim value if present otherwise returns null.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>The Name claim value, or null if the claim is not present.</returns>
        /// <remarks>The Name claim is identified by <see cref="CustomClaimTypes.UserName"/>.</remarks>
        public static string? GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            return principal.FindFirstValue(CustomClaimTypes.UserName);
        }

        /// <summary>
        /// Returns the Email claim value if present otherwise returns null.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>The Email claim value, or null if the claim is not present.</returns>
        /// <remarks>The Email claim is identified by <see cref="CustomClaimTypes.Email"/>.</remarks>
        public static string? GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            return principal.FindFirstValue(CustomClaimTypes.Email);
        }

        /// <summary>
        /// Returns true if the principal has an identity with the application cookie identity
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>True if the user is logged in with identity.</returns>
        public static bool IsSignedIn(this ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);
            return principal.Identities != null && principal.Identities.Any(i => i.AuthenticationType == AuthenticationType);
        }

        /// <summary>
		///     Get the the collection of all roles for a <see cref="ClaimsPrincipal"/>.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="principal" /> is <c>null</c>.</exception>
		/// <returns>A collection that including all the roles for the <paramref name="principal" />. If the user has no roles, this method will return a empty collection.</returns>
        public static IEnumerable<string> GetRoles(this ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);

            return principal.FindAll(CustomClaimTypes.Role).Select(i => i.Value);
            //return principal.FindAll(ClaimsIdentity.DefaultRoleClaimType).Select(i => i.Value);
        }

        public static bool IsInAnyRole(this ClaimsPrincipal principal, params string[] roles)
        {
            ArgumentNullException.ThrowIfNull(principal);
            return roles != null && roles.Any(principal.IsInRole);
        }

        public static DateTime? GetExpirationTime(this ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);

            //this[JwtRegisteredClaimNames.Exp] = EpochTime.GetIntDate(expires.Value.ToUniversalTime());
            var exp = principal.FindFirstValue(JwtRegisteredClaimNames.Exp);
            if (string.IsNullOrEmpty(exp)) return null;

            if (!long.TryParse(exp, out var secondsSinceUnixEpoch)) return null;
            return EpochTime.DateTime(secondsSinceUnixEpoch);
        }

        public static DateTime? GetUtcDateTime(this ClaimsPrincipal principal, string claimType)
        {
            ArgumentNullException.ThrowIfNull(principal);

            var claim = principal.FindFirstValue(claimType);
            if (long.TryParse(claim, out var expUnixTimestamp))
            {
                return DateTimeOffset.FromUnixTimeSeconds(expUnixTimestamp).UtcDateTime;
            }
            return null;
        }

        public static bool IsAuthorized<TController>(this ClaimsPrincipal user)
        {
            if (user == null)
                return false;

            var attributes = typeof(TController).GetCustomAttributes<AuthorizeAttribute>();
            if (attributes == null)
                return true;

            foreach (var attribute in attributes)
            {
                var roles = GetRoles(attribute);
                if (roles == null || roles.Length == 0)
                    continue;

                if (!user.IsInAnyRole(roles))
                {
                    return false;
                }
            }

            return true;
        }

        private static string[] GetRoles(AuthorizeAttribute attr) => attr.Roles?.Length > 0
            ? attr.Roles.Split(',')
            : [];
    }
}
