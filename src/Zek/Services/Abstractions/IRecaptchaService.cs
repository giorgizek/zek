using Zek.Contracts;

namespace Zek.Services.Abstractions
{
    public interface IRecaptchaService : IDisposable
    {
        string GetSiteKey();
        Task<ReCaptchaVerifyResponseDto?> VerifyAsync(string token, string? ip = null);
    }
}
