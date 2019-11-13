using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zek.Data.Repository
{
    public interface IEfRepository<TEntity> : IRepository<TEntity>  where TEntity : class
    {
        //todo Task<TEntity> FindAsync(params object[] keyValues);
        //todo Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        //todo Task<bool> RemoveAsync(params object[] keyValues);
        //todo Task<bool> RemoveAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken);
    }
}