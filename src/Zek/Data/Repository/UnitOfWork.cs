using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Zek.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly IDbContext Context;
        //private Hashtable _repositories;

        public UnitOfWork(IDbContext context)
        {
            Context = context;
        }

        public UnitOfWork()
        {
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }

        //todo public ICollection<ValidationResult> Commit()
        //{
        //    var validationResults = new List<ValidationResult>();

        //    try
        //    {
        //        Context.SaveChanges();
        //    }
        //    catch (DbEntityValidationException dbe)
        //    {
        //        foreach (var validation in dbe.EntityValidationErrors)
        //        {
        //            var validations = validation.ValidationErrors.Select(error => new ValidationResult(error.ErrorMessage, new[] { error.PropertyName }));

        //            validationResults.AddRange(validations);

        //            return validationResults;
        //        }
        //    }
        //    return validationResults;
        //}


        //public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IDisposable
        //{
        //    if (_repositories == null)
        //        _repositories = new Hashtable();

        //    var type = typeof(TEntity).Name;

        //    if (!_repositories.ContainsKey(type))
        //    {
        //        var repositoryType = typeof(Repository<>);
        //        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), Context);
        //        _repositories.Add(type, repositoryInstance);
        //    }

        //    return (IRepository<TEntity>)_repositories[type];
        //}


        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context?.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
