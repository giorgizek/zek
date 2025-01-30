namespace Zek.Model.DTO.Identity
{
    [Obsolete]
    public class RemoveLoginDTO
    {
        public string? LoginProvider { get; set; }
        public string? ProviderKey { get; set; }
    }
}
