using Zek.Contracts;
using Zek.PagedList;

namespace Zek.Modules.Services.Abstractions
{
    public interface IUserService<TUser, TFilter, TListItem, TUserDto>
    {
        Task<TUser?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<TUser?> FindByNameAsync(string? userName, int? customerId);
        Task<TUser?> FindByEmailAsync(string? email, int? customerId);
        Task<TUser?> PasswordSignInAsync(string userName, string password, int? customerId, bool isPersistent, bool lockoutOnFailure, bool bypassTwoFactor = false);
        Task UpdatePasswordAsync(TUser user, string password, CancellationToken cancellationToken = default);
        Task<IPagedList<TListItem>> GetAsync(TFilter filter, CancellationToken cancellation = default);
        Task<TUserDto?> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task<IApiResponse<int?>> SaveAsync(TUserDto model, CancellationToken cancellationToken = default);
        Task<IApiResponse<int>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
