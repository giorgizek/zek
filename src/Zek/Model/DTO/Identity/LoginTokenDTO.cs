namespace Zek.Model.DTO.Identity
{
    /// <summary>
    /// Represents a login token for the current user.
    /// </summary>
    [Obsolete]
    public class LoginTokenDTO : LoginTokenDTO<int> { }

    /// <summary>
    /// Represents a login token for a specific key type.
    /// </summary>
    [Obsolete]
    public class LoginTokenDTO<TKey>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public TKey Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the assigned roles.
        /// </summary>
        public string[]? Roles { get; set; }

        /// <summary>
        /// Gets or sets the login token.
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
    }
}