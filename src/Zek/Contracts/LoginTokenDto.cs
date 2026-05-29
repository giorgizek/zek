namespace Zek.Contracts
{
    /// <summary>
    /// Represents a login token payload for the default key type.
    /// </summary>
    public class LoginTokenDto : LoginTokenDto<int>
    {
    }

    /// <summary>
    /// Represents a login token payload.
    /// </summary>
    public class LoginTokenDto<TKey>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public TKey? Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Gets or sets the assigned roles.
        /// </summary>
        public string[]? Roles { get; set; }

        /// <summary>
        /// Gets or sets the token string.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the token expiration time.
        /// </summary>
        public DateTime? Expired { get; set; }

        /// <summary>
        /// Gets or sets the refresh token time.
        /// </summary>
        public DateTime? RefreshTokenTime { get; set; }

        /// <summary>
        /// Gets or sets the permission map.
        /// </summary>
        public Dictionary<int, int>? Permissions { get; set; }

        /// <summary>
        /// Gets or sets the access token value.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token value.
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}
