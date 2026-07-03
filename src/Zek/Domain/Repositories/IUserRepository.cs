using Zek.Domain.Entities.Identity;

namespace Zek.Domain.Repositories
{
    public interface IUserRepository<TEntity> : IRepository<TEntity>
        where TEntity : User
    {
        /// <summary>
        /// Finds a user by its identifier.
        /// </summary>
        Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds a user by email address.
        /// </summary>
        Task<TEntity?> FindByEmailAsync(string? email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the user name for the specified user identifier.
        /// </summary>
        Task<int?> FindIdByEmailAsync(string? email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds a user by user name.
        /// </summary>
        Task<TEntity?> FindByUserNameAsync(string? userName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the user identifier for the specified user name.
        /// </summary>
        Task<int?> FindIdByUserNameAsync(string? userName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the user name for the specified user identifier.
        /// </summary>
        Task<string?> GetUserNameByIdAsync(int userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines whether another user already uses the specified user name.
        /// </summary>
        Task<bool> IsDuplicatedUserNameAsync(int? id, string? userName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines whether another user already uses the specified email address.
        /// </summary>
        Task<bool> IsDuplicatedEmailAsync(int? id, string? email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines whether a user with the specified identifier exists.
        /// </summary>
        Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
