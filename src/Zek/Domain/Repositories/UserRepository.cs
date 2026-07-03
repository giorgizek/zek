using Microsoft.EntityFrameworkCore;
using System.Threading;
using Zek.Domain.Entities.Identity;
using Zek.Extensions;
using Zek.Persistence;

namespace Zek.Domain.Repositories
{

    public class UserRepository<TEntity> : Repository<TEntity>, IUserRepository<TEntity>
        where TEntity : User
    {
        public UserRepository(IDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Finds a user by its identifier.
        /// </summary>
        public Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Task.FromResult<TEntity?>(null);
            return Where(u => u.Id == id).SingleOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the user name for the specified user identifier.
        /// </summary>
        public Task<string?> GetUserNameByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Task.FromResult<string?>(null);

            return Where(u => u.Id == id)
                .Select(u => u.UserName)
                .SingleOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Finds a user by email address.
        /// </summary>
        public Task<TEntity?> FindByEmailAsync(string? email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(email))
                return Task.FromResult<TEntity?>(null);

            var normalizedEmail = email.NormalizeKey();
            return Where(u => u.NormalizedEmail == normalizedEmail).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the user identifier for the specified email address.
        /// </summary>
        public Task<int?> FindIdByEmailAsync(string? email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(email))
                return Task.FromResult<int?>(null);

            var normalizedEmail = email.NormalizeKey();
            return Where(u => u.NormalizedEmail == normalizedEmail).Select(u => (int?)u.Id).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Finds a user by user name.
        /// </summary>
        public Task<TEntity?> FindByUserNameAsync(string? userName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userName))
                return Task.FromResult<TEntity?>(null);

            var normalizedUserName = userName.NormalizeKey();
            return Where(u => u.NormalizedUserName == normalizedUserName).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the user identifier for the specified user name.
        /// </summary>
        public Task<int?> FindIdByUserNameAsync(string? userName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userName))
                return Task.FromResult<int?>(null);

            var normalizedUserName = userName.NormalizeKey();
            return Where(u => u.NormalizedUserName == normalizedUserName).Select(u => (int?)u.Id).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Determines whether another user already uses the specified user name.
        /// </summary>
        public Task<bool> IsDuplicatedUserNameAsync(int? id, string? userName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userName))
                return Task.FromResult(false);

            var normalizedUserName = userName.NormalizeKey();
            return Where(t => t.Id != id && t.NormalizedUserName == normalizedUserName).AnyAsync(cancellationToken);
        }

        /// <summary>
        /// Determines whether another user already uses the specified email address.
        /// </summary>
        public Task<bool> IsDuplicatedEmailAsync(int? id, string? email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(email))
                return Task.FromResult(false);

            var normalizedEmail = email.NormalizeKey();
            return Where(t => t.Id != id && t.NormalizedEmail == normalizedEmail).AnyAsync(cancellationToken);
        }

        /// <summary>
        /// Determines whether a user with the specified identifier exists.
        /// </summary>
        public Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id == 0)
                return Task.FromResult(false);

            return Where(t => t.Id == id).AnyAsync(cancellationToken);
        }
    }
}
