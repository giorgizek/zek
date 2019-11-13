using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Zek.Model.Dictionary;

namespace Zek.Data.Repository
{
    public interface IAutoNumberRepository : IRepository<AutoNumber>
    {
        int GetNextNumber(string name, int startNumber = 0);
        Task<int> GetNextNumberAsync(string name, int startNumber = 0);
    }

    public class AutoNumberRepository : Repository<AutoNumber>, IAutoNumberRepository
    {
        public AutoNumberRepository(IDbContext context) : base(context)
        {
        }


        public int GetNextNumber(string name, int startNumber = 0)
        {
            var autoNumber = Where(a => a.Name == name).FirstOrDefault();
            if (autoNumber == null)//if not exist for this name
            {
                autoNumber = new AutoNumber { Name = name, Number = startNumber + 1 };
                Add(autoNumber);
            }
            else
            {
                if (startNumber > autoNumber.Number)
                    autoNumber.Number = startNumber + 1;
                else
                    autoNumber.Number++;
            }

            Context.SaveChanges();
            return autoNumber.Number;
        }

        public async Task<int> GetNextNumberAsync(string name, int startNumber = 0)
        {
            var autoNumber = await Where(a => a.Name == name).FirstOrDefaultAsync();
            if (autoNumber == null)//if not exist for this name
            {
                autoNumber = new AutoNumber { Name = name, Number = startNumber + 1 };
                Add(autoNumber);
            }
            else
            {
                if (startNumber > autoNumber.Number)
                    autoNumber.Number = startNumber + 1;
                else
                    autoNumber.Number++;
            }

            await Context.SaveChangesAsync();
            return autoNumber.Number;
        }
    }
}
