using Zek.Domain.Entities.Identity;

namespace Zek.Domain.Repositories
{
    public interface IUserRepository<TEntity> : IRepository<TEntity>
        where TEntity : User
    {
        Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<TEntity?> FindByEmailAsync(string? email, CancellationToken cancellationToken = default);
        Task<int?> FindIdByEmailAsync(string? email, CancellationToken cancellationToken = default);
        Task<TEntity?> FindByUserNameAsync(string? userName, CancellationToken cancellationToken = default);
        Task<int?> FindIdByUserNameAsync(string? userName, CancellationToken cancellationToken = default);
        Task<string?> GetUserNameByIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<bool> IsDuplicatedUserNameAsync(int? id, string? userName, CancellationToken cancellationToken = default);
        Task<bool> IsDuplicatedEmailAsync(int? id, string? email, CancellationToken cancellationToken = default);
        Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
