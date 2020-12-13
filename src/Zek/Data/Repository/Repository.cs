using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zek.Data.Repository
{
    public class Repository : IDisposable
    {
        public Repository(IDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        protected IDbContext Context;


        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                //try
                //{
                //    if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
                //    {
                //        _objectContext.Connection.Close();
                //    }
                //}
                //catch (ObjectDisposedException)
                //{
                //    // do nothing, the objectContext has already been disposed
                //}

                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }

            _disposed = true;
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    public class Repository<TEntity> : Repository, IRepository<TEntity> where TEntity : class
    {
        public Repository(IDbContext context) : base(context)
        {
            _dbSet = Context.Set<TEntity>();
        }

        protected readonly DbSet<TEntity> _dbSet;



        //protected virtual string GetConnectionString()
        //{
        //    //context.Database.AsSqlServer().Connection.DbConnection;
        //    return (Context as DbContext).Database.GetDbConnection().ConnectionString;
        //}

        public virtual IQueryable<TEntity> GetAll() => _dbSet;


        //private IQueryable<TEntity> GetInternal(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    includeProperties?.ForEach(includeProperty => query.Include(includeProperty));

        //    if (filter != null)
        //        query = query.Where(filter);

        //    if (orderBy != null)
        //        query = orderBy(query);

        //    if (page != null && pageSize != null)
        //        query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

        //    return query;
        //}
        //public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null) => GetInternal(filter, orderBy, includeProperties, page, pageSize).ToList();
        //public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null) => await GetInternal(filter, orderBy, includeProperties, page, pageSize).ToListAsync();

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate);

        //public virtual TEntity Single(Expression<Func<TEntity, bool>> whereCondition) => _dbSet.Single(whereCondition);
        //public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereCondition) => _dbSet.SingleOrDefault(whereCondition);

        //public virtual TEntity First(Expression<Func<TEntity, bool>> predicate) => _dbSet.First(predicate);
        //public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => _dbSet.FirstOrDefault(predicate);

        public virtual void Add(TEntity entity) => _dbSet.Add(entity);
        public virtual void AddRange(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void Detach(TEntity entity) => Context.Entry(entity).State = EntityState.Detached;

        public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in _dbSet.Where(predicate))
            {
                Remove(entity);
            }
        }
        public virtual void Remove(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }



        public virtual Task<int> SaveAsync() => Context.SaveChangesAsync();




    }
}

