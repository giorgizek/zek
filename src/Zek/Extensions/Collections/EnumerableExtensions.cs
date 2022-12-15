using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zek.Extensions.Collections
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Checks whether <paramref name="enumerable"/> is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> to be checked.</param>
        /// <returns>True if <paramref name="enumerable"/> is null or empty, false otherwise.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> collection) where T : class
        {
            foreach (var item in collection)
            {
                if (item != null)
                    yield return item;
            }
        }

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> collection) where T : struct
        {
            foreach (var item in collection)
            {
                if (item.HasValue)
                    yield return item.Value;
            }
        }

        public static IEnumerable<T> And<T>(this IEnumerable<T> collection, T newItem)
        {
            foreach (var item in collection)
                yield return item;
            yield return newItem;
        }

        public static IEnumerable<T> PreAnd<T>(this IEnumerable<T> collection, T newItem)
        {
            yield return newItem;
            foreach (var item in collection)
                yield return item;
        }

        public static int IndexOf<T>(this IEnumerable<T> collection, T item)
        {
            var i = 0;
            foreach (var val in collection)
            {
                if (EqualityComparer<T>.Default.Equals(item, val))
                    return i;
                i++;
            }
            return -1;
        }

        public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> condition)
        {
            var i = 0;
            foreach (var val in collection)
            {
                if (condition(val))
                    return i;
                i++;
            }
            return -1;
        }

        public static string ToString<T>(this IEnumerable<T> collection, string separator)
        {
            var sb = new StringBuilder();
            foreach (var item in collection)
            {
                sb.Append(item);
                sb.Append(separator);
            }
            return sb.ToString(0, Math.Max(0, sb.Length - separator.Length));  // Remove at the end is faster
        }


        public static string ToString<T>(this IEnumerable<T> collection, Func<T, string> toString, string separator)
        {
            var sb = new StringBuilder();
            foreach (var item in collection)
            {
                sb.Append(toString(item));
                sb.Append(separator);
            }
            return sb.ToString(0, Math.Max(0, sb.Length - separator.Length));  // Remove at the end is faster
        }
    }
}
