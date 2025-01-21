using Microsoft.EntityFrameworkCore;

namespace Zek.Persistence
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        //EntityEntry Entry(object o);
        void Dispose();
    }
}
