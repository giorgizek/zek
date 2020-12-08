using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zek.Model.Config;

namespace Zek.Utils
{
    public static class JwtHelper
    {
        public static string Create(string userId, string userName, DateTime? expires, IEnumerable<string> roles, TokenOptions options)
            => Create(
                 options.ValidIssuer,
                 options.ValidAudience,
                 ClaimHelper.Create(userId, userName, roles),
                 expires,
                 options.IssuerSigningKey);

        public static string Create(string userId, string userName, DateTime? expires, IEnumerable<string> roles, IEnumerable<KeyPair<string, string>> claimCollection, TokenOptions options)
            => Create(
                options.ValidIssuer,
                options.ValidAudience,
                ClaimHelper.Create(userId, userName, roles, claimCollection),
                expires,
                options.IssuerSigningKey);

        private static string Create(string issuer, string audience, IEnumerable<Claim> claims, DateTime? expires, string key)
        {
            var symKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(symKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);
            var jwtToken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
