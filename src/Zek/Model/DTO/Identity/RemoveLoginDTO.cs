namespace Zek.Model.DTO.Identity
{
    /// <summary>
    /// Represents a request to remove an external login provider.
    /// </summary>
    [Obsolete]
    public class RemoveLoginDTO
    {
        /// <summary>
        /// Gets or sets the login provider name.
        /// </summary>
        public string? LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        public string? ProviderKey { get; set; }
    }
}
