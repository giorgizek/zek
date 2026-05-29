namespace Zek.Model.DTO.Identity
{
    /// <summary>
    /// Represents a login request that uses an email address.
    /// </summary>
    public class EmailLoginDTO : LoginBaseDTO
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string? Email { get; set; }

    }
}
