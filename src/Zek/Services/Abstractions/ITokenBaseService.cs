namespace Zek.Services.Abstractions
{
    public interface ITokenBaseService
    {
        int GetUserId();
        string? GetUserName();
        DateTime GetExpirationTime();
        IEnumerable<string> GetRoles();
        string? GetIpAddress();
    }
}
