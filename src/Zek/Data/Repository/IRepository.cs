using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zek.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class// , IDisposable
    {
        IQueryable<TEntity> GetAll();

        //IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null);
        //Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        //TEntity Single(Expression<Func<TEntity, bool>> predicate);
        //TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        //TEntity First(Expression<Func<TEntity, bool>> predicate);
        //TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        //TEntity FirstOrDefault();

        //int Count();
        //int Count(Expression<Func<TEntity, bool>> predicate);
        //bool Any();
        //bool Any(Expression<Func<TEntity, bool>> predicate);



        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        //void Update(TEntity entityToUpdate);
        void Detach(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);


        //void Save();

        //Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
