namespace Zek.Model.DTO.Identity
{
    /// <summary>
    /// Represents a request to verify a phone number.
    /// </summary>
    [Obsolete]
    public class VerifyPhoneNumberDTO
    {
        /// <summary>
        /// Gets or sets the verification code.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
