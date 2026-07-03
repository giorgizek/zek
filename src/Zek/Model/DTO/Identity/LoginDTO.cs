namespace Zek.Model.DTO.Identity
{
    /// <summary>
    /// Represents a login request that uses a user name.
    /// </summary>
    [Obsolete]
    public class LoginDTO : LoginBaseDTO
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string? UserName { get; set; }
    }
}