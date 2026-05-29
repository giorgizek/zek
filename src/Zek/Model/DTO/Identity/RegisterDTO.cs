namespace Zek.Model.DTO.Identity
{
    /// <summary>
    /// Represents a registration request.
    /// </summary>
    [Obsolete]
    public class RegisterBaseDTO : CaptchaDTO
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmation password.
        /// </summary>
        public string? ConfirmPassword { get; set; }
    }
}
