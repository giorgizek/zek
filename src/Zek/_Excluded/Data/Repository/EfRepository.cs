using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Zek.Data.Repository
{
    public class EfRepository<TEntity> : Repository<TEntity>, IEfRepository<TEntity> where TEntity : class
    {
        public EfRepository(IDbContext context) : base(context)
        {
            //todo var db = context as DbContext;

            //todo if (db != null)
            //{
            //    _dbSet = db.Set<TEntity>();
            //}
        }
        //todo private readonly DbSet<TEntity> _dbSet;


        //todo public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        //{
        //    return await _dbSet.FindAsync(keyValues);
        //}

        //todo public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        //{
        //    return await _dbSet.FindAsync(cancellationToken, keyValues);
        //}

        //todo public virtual async Task<bool> RemoveAsync(params object[] keyValues)
        //{
        //    return await RemoveAsync(CancellationToken.None, keyValues);
        //}

        //todo public virtual async Task<bool> RemoveAsync(CancellationToken cancellationToken, params object[] keyValues)
        //{
        //    var entityToDelete = await FindAsync(cancellationToken, keyValues);

        //    if (entityToDelete == null)
        //        return false;

        //    Remove(entityToDelete);

        //    //todo entity.ObjectState = ObjectState.Deleted;
        //    //todo _dbSet.Attach(entityToDelete);

        //    return true;
        //}

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await All.FirstOrDefaultAsync(predicate);
        }
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await All.FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public virtual async Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken)
        {
            return await All.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
