using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Zek.Security.Claims;

namespace Zek.Utils
{
    public static class ClaimHelper
    {
        public static List<Claim> Create(
            string userId,
            string userName = null,
            //string email = null,
            //string givenName = null,
            //string surname = null,
            IEnumerable<string> roles = null,
            IEnumerable<KeyPair<string, string>> claimCollection = null)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID parameter is required.", nameof(userId));
            //if (string.IsNullOrEmpty(userName))
            //    throw new ArgumentException("UserName parameter is required.", nameof(userName));

            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.CookiePath, ".contoso.com"),//CookieDomain - the domain name the cookie will be served to. By default this is the host name the request was sent to. The browser will only serve the cookie to a matching host name. You may wish to adjust this to have cookies available to any host in your domain. For example setting the cookie domain to .contoso.com will make it available to contoso.com, www.contoso.com, staging.www.contoso.com etc.
                new(CustomClaimTypes.UserId, userId),
            };

            if (!string.IsNullOrEmpty(userName))
            {
                claims.Add(new Claim(CustomClaimTypes.UserName, userName));
            }

            //if (!string.IsNullOrEmpty(email))
            //    claims.Add(new Claim(ClaimTypes.Email, email));
            //if (!string.IsNullOrEmpty(givenName))
            //    claims.Add(new Claim(ClaimTypes.GivenName, givenName));
            //if (!string.IsNullOrEmpty(surname))
            //    claims.Add(new Claim(ClaimTypes.Surname, surname));
            //if (!string.IsNullOrEmpty(groupSid))
            //    claims.Add(new Claim(ClaimTypes.GroupSid, groupSid));
            if (roles != null)
                claims.AddRange(roles.Select(role => new Claim(CustomClaimTypes.Role, role)));

            if (claimCollection != null)
            {
                claims.AddRange(claimCollection.Select(c => new Claim(c.Key, c.Value)));
            }


            return claims;
        }



        /*/// <summary>
        ///     Convert the key to a string, by default just calls .ToString()
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string ConvertIdToString(object key)
        {
            if (key == null || key.IsDefault())
            {
                return null;
            }
            return key.ToString();
        }*/
    }
}
