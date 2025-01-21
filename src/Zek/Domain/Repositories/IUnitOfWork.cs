namespace Zek.Domain.Repositories
{
    public interface IUnitOfWork
    {
        void Save();
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
