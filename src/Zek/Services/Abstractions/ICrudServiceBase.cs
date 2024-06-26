using System.Threading;
using System.Threading.Tasks;
using Zek.Contracts;
using Zek.PagedList;

namespace Zek.Services.Abstractions
{
    public interface ICrudServiceBase<TFilter, TListItem, TItem>
        : ICrudServiceBase<int, TFilter, TListItem, TItem>
    {
    }
    public interface ICrudServiceBase<TId, TFilter, TListItem, TItem>
        : ICrudServiceBase<TId, TFilter, TListItem, TItem, IApiResponse, IApiResponse>
    {
    }

    public interface ICrudServiceBase<TId, TFilter, TListItem, TItem, TSaveResponse, TDeleteResponse>
    {
        Task<IPagedList<TListItem>> GetAsync(TFilter filter, CancellationToken cancellationToken = default);
        Task<TItem?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<TSaveResponse> SaveAsync(TItem model, CancellationToken cancellationToken = default);
        Task<TDeleteResponse> DeleteAsync(TId id, CancellationToken cancellationToken = default);
    }
}