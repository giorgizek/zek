using Zek.Contracts;

namespace Zek.Services.Abstractions
{
    public interface ISmsSender
    {
        Task<IApiResponse> SendAsync(string phoneNumber, string message);
    }
}
