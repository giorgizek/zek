namespace Zek.Model.DTO.Identity
{
    /// <summary>
    /// Represents the base data required to authenticate a user.
    /// </summary>
    [Obsolete]
    public class LoginBaseDTO : CaptchaDTO
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current session should be remembered.
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// Gets or sets the client IP address.
        /// </summary>
        public string? Ip { get; set; }
    }
}