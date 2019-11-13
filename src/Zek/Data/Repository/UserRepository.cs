using System;
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
        Task<TEntity> FindByIdAsync(int? id);
        Task<TEntity> FindByEmailAsync(string email);
        Task<int?> FindIdByEmailAsync(string email);
        Task<TEntity> FindByUserNameAsync(string userName);
        Task<int?> FindIdByUserNameAsync(string userName);


        Task<string> GetUserNameByIdAsync(int? userId);
    }

    public class UserRepository<TEntity> : Repository<TEntity>, IUserRepository<TEntity>
        where TEntity : User
    {
        public UserRepository(IDbContext context)
            : base(context)
        {
        }

        public async Task<TEntity> FindByIdAsync(int? id)
        {
            if (id.GetValueOrDefault() <= 0)
                return null;
            return await Where(u => u.Id == id).SingleOrDefaultAsync();
        }


        public async Task<string> GetUserNameByIdAsync(int? id)
        {
            if (id.GetValueOrDefault() <= 0)
                return null;

            return await Where(u => u.Id == id)
                .Select(u => u.UserName)
                .SingleOrDefaultAsync();
        }

        public async Task<TEntity> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var normalized = email.NormalizeKey();
            return await Where(u => u.NormalizedEmail == normalized).FirstOrDefaultAsync();
        }

        public async Task<int?> FindIdByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var normalized = email.NormalizeKey();
            return await Where(u => u.NormalizedEmail == normalized).Select(u => (int?)u.Id).FirstOrDefaultAsync();
        }


        public async Task<TEntity> FindByUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            var normalized = userName.NormalizeKey();
            return await Where(u => u.NormalizedUserName == normalized).FirstOrDefaultAsync();
        }

        public async Task<int?> FindIdByUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            var normalized = userName.NormalizeKey();
            return await Where(u => u.NormalizedUserName == normalized).Select(u => (int?)u.Id).FirstOrDefaultAsync();
        }
    }
}
