using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zek.Data.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        //void Dispose();
        void Save();
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
        //void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IDisposable;
    }
}
