using Zek.Persistence;

namespace Zek.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly IDbContext Db;
        //private Hashtable _repositories;

        public UnitOfWork(IDbContext context)
        {
            Db = context;
        }

        public void Save()
        {
            Db.SaveChanges();
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return Db.SaveChangesAsync(cancellationToken);
        }

        //private bool _disposed;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            Db?.Dispose();
        //        }
        //    }
        //    _disposed = true;
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //~UnitOfWork()
        //{
        //    Dispose(false);
        //}
    }
}
