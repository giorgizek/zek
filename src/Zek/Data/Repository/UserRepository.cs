using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zek.Extensions;
using Zek.Model.Identity;

namespace Zek.Data.Repository
{

    public interface IUserRepository<TEntity> : IRepository<TEntity>
        where TEntity : User
    {
        Task<TEntity> FindByIdAsync(int id);
        Task<TEntity> FindByEmailAsync(string email);
        Task<int?> FindIdByEmailAsync(string email);
        Task<TEntity> FindByUserNameAsync(string userName);
        Task<int?> FindIdByUserNameAsync(string userName);
        Task<string> GetUserNameByIdAsync(int userId);
        Task<bool> IsDuplicatedUserNameAsync(int? id, string userName);
        Task<bool> IsDuplicatedEmailAsync(int? id, string email);
        Task<bool> IsExistsAsync(int id);
    }

    public class UserRepository<TEntity> : Repository<TEntity>, IUserRepository<TEntity>
        where TEntity : User
    {
        public UserRepository(IDbContext context)
            : base(context)
        {
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            if (id <= 0)
                return null;
            return await Where(u => u.Id == id).SingleOrDefaultAsync();
        }

        public async Task<string> GetUserNameByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await Where(u => u.Id == id)
                .Select(u => u.UserName)
                .SingleOrDefaultAsync();
        }

        public async Task<TEntity> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var normalizedEmail = email.NormalizeKey();
            return await Where(u => u.NormalizedEmail == normalizedEmail).FirstOrDefaultAsync();
        }

        public async Task<int?> FindIdByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var normalizedEmail = email.NormalizeKey();
            return await Where(u => u.NormalizedEmail == normalizedEmail).Select(u => (int?)u.Id).FirstOrDefaultAsync();
        }

        public async Task<TEntity> FindByUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            var normalizedUserName = userName.NormalizeKey();
            return await Where(u => u.NormalizedUserName == normalizedUserName).FirstOrDefaultAsync();
        }

        public async Task<int?> FindIdByUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            var normalizedUserName = userName.NormalizeKey();
            return await Where(u => u.NormalizedUserName == normalizedUserName).Select(u => (int?)u.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> IsDuplicatedUserNameAsync(int? id, string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return false;

            var normalizedUserName = userName.NormalizeKey();
            return await Where(t => t.Id != id && t.NormalizedUserName == normalizedUserName).AnyAsync();
        }

        public async Task<bool> IsDuplicatedEmailAsync(int? id, string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var normalizedEmail = email.NormalizeKey();
            return await Where(t => t.Id != id && t.NormalizedEmail == normalizedEmail).AnyAsync();
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            if (id == 0)
                return false;

            return await Where(t => t.Id == id).AnyAsync();
        }
    }
}
