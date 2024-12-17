using System.Collections.Generic;
using System;

namespace Zek.Contracts
{
    public class LoginTokenDto : LoginTokenDto<int>
    {
    }

    public class LoginTokenDto<TKey>
    {
        public TKey Id { get; set; }
        public string UserName { get; set; }
        public string[] Roles { get; set; }
        public string Token { get; set; }
        public DateTime? Expired { get; set; }
        public DateTime? RefreshTokenTime { get; set; }
        public Dictionary<int, int> Permissions { get; set; }

        /// <summary>
        /// Access Token Expiration: Access tokens should have a short lifespan (e.g., 15 minutes to 1 hour).
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Refresh Token Expiration: Refresh tokens should have a longer lifespan, such as 1 week, 1 month, or more.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
