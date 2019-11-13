using Zek.Model.Licensing;

namespace Zek.Data.Repository
{
    public interface ILicenseRepository : IRepository<License>
    {

    }

    public class LicenseRepository : Repository<License>, ILicenseRepository
    {
        public LicenseRepository(IDbContext context) : base(context)
        {
        }
    }
}
