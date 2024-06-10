using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zek.PagedList
{
    public static partial class PagedListExtensions
    {
        /// <summary>
        /// Async creates a subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>        
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <returns>A subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.</returns>
        /// <seealso cref="PagedList{T}"/>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        {
            var subset = new List<T>();
            var totalCount = 0;

            if (superset != null)
            {
                totalCount = superset.Count();

                subset.AddRange(
                    (pageNumber == 1)
                        ? superset.Skip(0).Take(pageSize).ToList()
                        : superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
                );
            }

            return new PagedList<T>(subset, pageNumber, pageSize, totalCount);
        }

        /// <summary>
        /// Async creates a subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>        
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.</returns>
        /// <seealso cref="PagedList{T}"/>
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> superset, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var subset = new List<T>();
            var totalCount = 0;

            if (superset != null)
            {
                totalCount = await superset.CountAsync(cancellationToken);

                subset.AddRange(
                    (pageNumber == 1)
                        ? await superset.Skip(0).Take(pageSize).ToListAsync(cancellationToken)
                        : await superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken)
                );
            }

            return new PagedList<T>(subset, pageNumber, pageSize, totalCount);
        }


        /// <summary>
        /// Async creates a subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>        
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <returns>A subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.</returns>
        /// <seealso cref="PagerListDTO{T}"/>
        [Obsolete("Please use ToPagedList", true)]
        public static PagerListDTO<T> ToPagerListDTO<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        {
            var subset = new List<T>();
            var totalCount = 0;

            if ((superset != null))
            {
                totalCount = superset.Count();
                if (totalCount > 0)
                {
                    subset.AddRange(pageNumber == 1
                            ? superset.Skip(0).Take(pageSize).ToList()
                            : superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
                    );
                }
            }

            return new PagerListDTO<T>(new Pager { TotalItemCount = totalCount }, subset);
        }

        /// <summary>
        /// Async creates a subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>        
        /// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.</returns>
        /// <seealso cref="PagerListDTO{T}"/>
        [Obsolete("Please use ToPagedListAsync", true)]
        public static async Task<PagerListDTO<T>> ToPagerListDTOAsync<T>(this IQueryable<T> superset, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var subset = new List<T>();
            var totalCount = 0;

            if ((superset != null))
            {
                totalCount = await superset.CountAsync(cancellationToken);
                if (totalCount > 0)
                {
                    subset.AddRange(pageNumber == 1
                        ? await superset.Skip(0).Take(pageSize).ToListAsync(cancellationToken)
                        : await superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken)
                    );
                }
            }

            return new PagerListDTO<T>(new Pager { TotalItemCount = totalCount }, subset);
        }
    }
}
