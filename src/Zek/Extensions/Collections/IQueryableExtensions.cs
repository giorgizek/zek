using Microsoft.EntityFrameworkCore;

namespace Zek.Extensions.Collections
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Execute the remote query and materialize the results.
        /// </summary>
        /// <typeparam name="T">The type of the query.</typeparam>
        /// <param name="query">The base query to run.</param>
        /// <returns>The result.</returns>
        public static async Task<HashSet<T>> ToHashSetAsync<T>(this IQueryable<T> query) => new HashSet<T>(await query.ToArrayAsync());
    }
}
