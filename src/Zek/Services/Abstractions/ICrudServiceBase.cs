using System.Threading;
using System.Threading.Tasks;
using Zek.Contracts;
using Zek.PagedList;

namespace Zek.Services.Abstractions
{
    public interface ICrudServiceBase<TFilter, TListItem, TItem>
        : ICrudServiceBase<TFilter, TListItem, TItem, IApiResponse>
    {
    }

    public interface ICrudServiceBase<TFilter, TListItem, TItem, TSaveResponse>
    {
        Task<IPagedList<TListItem>> GetAsync(TFilter filter, CancellationToken cancellationToken = default);
        Task<TItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<TSaveResponse> SaveAsync(TItem model, CancellationToken cancellationToken = default);
    }
}