﻿using System.Linq;
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
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
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

        /// <summary>
        /// Returns all distinct elements of the given source, where "distinctness"
        /// is determined via a projection and the specified comparer for the projected type.
        /// </summary>
        /// <remarks>
        /// This operator uses deferred execution and streams the results, although
        /// a set of already-seen keys is retained. If a key is seen multiple times,
        /// only the first element with that key is returned.
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="keySelector">Projection for determining "distinctness"</param>
        /// <param name="comparer">The equality comparer to use to determine whether or not keys are equal.
        /// If null, the default equality comparer for <c>TSource</c> is used.</param>
        /// <returns>A sequence consisting of distinct elements from the source sequence,
        /// comparing them by the specified key projection.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer = null)
        {
            var knownKeys = new HashSet<TKey>(comparer);
            foreach (var element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                    yield return element;
            }
        }
    }
}



//public static class EnumerableExtensions
//{
//    /// <summary>
//    /// Checks whether <paramref name="enumerable"/> is null or empty.
//    /// </summary>
//    /// <typeparam name="T">The type of the <paramref name="enumerable"/>.</typeparam>
//    /// <param name="enumerable">The <see cref="IEnumerable{T}"/> to be checked.</param>
//    /// <returns>True if <paramref name="enumerable"/> is null or empty, false otherwise.</returns>
//    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
//    {
//        return enumerable == null || !enumerable.Any();
//    }

//    public static IEnumerable<T> NotNull<T>(this IEnumerable<T> collection) where T : class
//    {
//        foreach (var item in collection)
//        {
//            if (item != null)
//                yield return item;
//        }
//    }

//    public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> collection) where T : struct
//    {
//        foreach (var item in collection)
//        {
//            if (item.HasValue)
//                yield return item.Value;
//        }
//    }

//    public static IEnumerable<T> And<T>(this IEnumerable<T> collection, T newItem)
//    {
//        foreach (var item in collection)
//            yield return item;
//        yield return newItem;
//    }

//    public static IEnumerable<T> PreAnd<T>(this IEnumerable<T> collection, T newItem)
//    {
//        yield return newItem;
//        foreach (var item in collection)
//            yield return item;
//    }

//    public static int IndexOf<T>(this IEnumerable<T> collection, T item)
//    {
//        var i = 0;
//        foreach (var val in collection)
//        {
//            if (EqualityComparer<T>.Default.Equals(item, val))
//                return i;
//            i++;
//        }
//        return -1;
//    }

//    public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> condition)
//    {
//        var i = 0;
//        foreach (var val in collection)
//        {
//            if (condition(val))
//                return i;
//            i++;
//        }
//        return -1;
//    }

//    public static string ToString<T>(this IEnumerable<T> collection, string separator)
//    {
//        var sb = new StringBuilder();
//        foreach (var item in collection)
//        {
//            sb.Append(item);
//            sb.Append(separator);
//        }
//        return sb.ToString(0, Math.Max(0, sb.Length - separator.Length));  // Remove at the end is faster
//    }


//    public static string ToString<T>(this IEnumerable<T> collection, Func<T, string> toString, string separator)
//    {
//        var sb = new StringBuilder();
//        foreach (var item in collection)
//        {
//            sb.Append(toString(item));
//            sb.Append(separator);
//        }
//        return sb.ToString(0, Math.Max(0, sb.Length - separator.Length));  // Remove at the end is faster
//    }
//}