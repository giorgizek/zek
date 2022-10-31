using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zek.Data.Repository
{
    public class Repository// : IDisposable
    {
        public Repository(IDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        protected IDbContext Context;
    }


    public class Repository<TEntity> : Repository, IRepository<TEntity> where TEntity : class
    {
        public Repository(IDbContext context) : base(context)
        {
            _dbSet = Context.Set<TEntity>();
        }

        protected readonly DbSet<TEntity> _dbSet;
        public virtual IQueryable<TEntity> GetAll() => _dbSet;
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate);

        public virtual void Add(TEntity entity) => _dbSet.Add(entity);
        public virtual void AddRange(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);
        public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in _dbSet.Where(predicate))
            {
                Remove(entity);
            }
        }
        public virtual void Remove(TEntity entityToDelete)
        {
            //if (Context.Entry(entityToDelete).State == EntityState.Detached)
            //{
            //    _dbSet.Attach(entityToDelete);
            //}
            _dbSet.Remove(entityToDelete);
        }
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}

