using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zek.Extensions.Collections
{
    public static class IEnumerableExtensions
    {
        //public static IEnumerable<T> ConvertAll<T>(this IEnumerable en, Converter<object, T> converter)
        //{
        //    var enumerator = en.GetEnumerator();
        //    while (enumerator.MoveNext())
        //    {
        //        var current = enumerator.Current;
        //        yield return converter(current);
        //    }
        //}



        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (T element in source)
            {
                action(element);
            }
        }


        public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));
            foreach (var element in source)
            {
                await func(element);
            }
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
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            var knownKeys = new HashSet<TKey>(comparer);
            foreach (var element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                    yield return element;
            }

            //return _(); IEnumerable<TSource> _()
            //{
            //    var knownKeys = new HashSet<TKey>(comparer);
            //    foreach (var element in source)
            //    {
            //        if (knownKeys.Add(keySelector(element)))
            //            yield return element;
            //    }
            //}
        }



        /// <summary>
        /// Check if any element match on both arrays
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static bool Any(this IEnumerable<string> array1, string[] array2)
        {
            return array1.Any(array2.Contains);
        }

        /// <summary>
        /// Check if any element match on both arrays
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool Any(this IEnumerable<string> array1, string[] array2, StringComparer comparer)
        {
            return array1.Any(x => array2.Contains(x, comparer));
        }
    }
}
