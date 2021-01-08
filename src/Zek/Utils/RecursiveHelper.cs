using System;
using System.Collections.Generic;
using System.Linq;

namespace Zek.Utils
{
    public static class RecursiveHelper
    {
        public static List<TSource> SelectTree<TSource, TKey>(List<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TKey> parentKeySelector, Func<TSource, ICollection<TSource>> childrenSelector)
        {
            return new(InternalSelectTree(source, keySelector, parentKeySelector, childrenSelector));
        }
        public static TSource[] SelectTree<TSource, TKey>(TSource[] source, Func<TSource, TKey> keySelector, Func<TSource, TKey> parentKeySelector, Func<TSource, ICollection<TSource>> childrenSelector)
        {
            return InternalSelectTree(source, keySelector, parentKeySelector, childrenSelector).ToArray();
        }
        public static IEnumerable<TSource> SelectTree<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TKey> parentKeySelector, Func<TSource, ICollection<TSource>> childrenSelector)
        {
            return InternalSelectTree(source, keySelector, parentKeySelector, childrenSelector);
        }
        public static ICollection<TSource> SelectTree<TSource, TKey>(ICollection<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TKey> parentKeySelector, Func<TSource, ICollection<TSource>> childrenSelector)
        {
            return new List<TSource>(InternalSelectTree(source, keySelector, parentKeySelector, childrenSelector));
        }

        private static IEnumerable<TSource> InternalSelectTree<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TKey> parentKeySelector, Func<TSource, ICollection<TSource>> childrenSelector, TKey key = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (parentKeySelector == null)
                throw new ArgumentNullException(nameof(parentKeySelector));
            if (childrenSelector == null)
                throw new ArgumentNullException(nameof(parentKeySelector));
            if (keySelector == parentKeySelector)
                throw new ArgumentException("keySelector equals parentKeySelector");

            source = new List<TSource>(source);
            var subs = new List<TSource>();
            foreach (var item in source)
            {
                var parentKey = parentKeySelector(item);
                if (parentKey.Equals(key))
                {
                    subs.Add(item);
                }
            }

            foreach (var sub in subs)
            {
                var children = childrenSelector(sub);
                if (children == null)
                    continue;

                var subKey = keySelector(sub);
                var subChildren = InternalSelectTree(source, keySelector, parentKeySelector, childrenSelector, subKey);
                if (subChildren == null) continue;
                foreach (var subChild in subChildren)
                {
                    children.Add(subChild);
                }
            }

            return subs;
        }

        /// <summary>
        ///   This method extends the LINQ methods to flatten a collection of 
        ///   items that have a property of children of the same type.
        /// </summary>
        /// <typeparam name = "T">Item type.</typeparam>
        /// <param name = "source">Source collection.</param>
        /// <param name = "childPropertySelector">
        ///   Child property selector delegate of each item. 
        ///   IEnumerable'T' childPropertySelector(T itemBeingFlattened)
        /// </param>
        /// <returns>Returns a one level list of elements of type T.</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childPropertySelector)
        {
            return source.Flatten((itemBeingFlattened, objectsBeingFlattened) => childPropertySelector(itemBeingFlattened));
        }

        /// <summary>
        ///   This method extends the LINQ methods to flatten a collection of 
        ///   items that have a property of children of the same type.
        /// </summary>
        /// <typeparam name = "TSource">Item type.</typeparam>
        /// <param name = "source">Source collection.</param>
        /// <param name = "childPropertySelector">
        ///   Child property selector delegate of each item. 
        ///   IEnumerable'T' childPropertySelector
        ///   (T itemBeingFlattened, IEnumerable'T' objectsBeingFlattened)
        /// </param>
        /// <returns>Returns a one level list of elements of type T.</returns>
        public static IEnumerable<TSource> Flatten<TSource>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TSource>, IEnumerable<TSource>> childPropertySelector)
        {
            var enumerable = source as IList<TSource> ?? source.ToList();
            return enumerable.Concat(enumerable.Where(item => childPropertySelector(item, enumerable) != null).SelectMany(itemBeingFlattened => childPropertySelector(itemBeingFlattened, enumerable).Flatten(childPropertySelector)));
        }



        //public static IEnumerable<T> FlattenTogether<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childPropertySelector)
        //{
        //    return source.FlattenTogether((itemBeingFlattened, objectsBeingFlattened) => childPropertySelector(itemBeingFlattened));
        //}
        //public static IEnumerable<TSource> FlattenTogether<TSource>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TSource>, IEnumerable<TSource>> childPropertySelector)
        //{
        //    var list = new List<TSource>();
        //    foreach (var item in source)
        //    {
        //        list.Add(item);

        //    }

        //    return list;
        //}


        ///// <summary>
        ///// This would flatten out a recursive data structure ignoring the loops. The end result would be an enumerable which enumerates all the
        ///// items in the data structure regardless of the level of nesting.
        ///// </summary>
        ///// <typeparam name="T">Type of the recursive data structure</typeparam>
        ///// <param name="source">Source element</param>
        ///// <param name="childrenSelector">a function that returns the children of a given data element of type T</param>
        ///// <param name="keySelector">a function that returns a key value for each element</param>
        ///// <returns>a faltten list of all the items within recursive data structure of T</returns>
        //public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector, Func<T, object> keySelector) where T : class
        //{
        //    if (source == null)
        //        throw new ArgumentNullException("source");
        //    if (childrenSelector == null)
        //        throw new ArgumentNullException("childrenSelector");
        //    if (keySelector == null)
        //        throw new ArgumentNullException("keySelector");
        //    var stack = new Stack<T>(source);
        //    var dictionary = new Dictionary<object, T>();
        //    while (stack.Any())
        //    {
        //        var currentItem = stack.Pop();
        //        var currentkey = keySelector(currentItem);
        //        if (dictionary.ContainsKey(currentkey) == false)
        //        {
        //            dictionary.Add(currentkey, currentItem);
        //            var children = childrenSelector(currentItem);
        //            if (children != null)
        //            {
        //                foreach (var child in children)
        //                {
        //                    stack.Push(child);
        //                }
        //            }
        //        }
        //        yield return currentItem;
        //    }
        //}

        ///// <summary>
        ///// This would flatten out a recursive data structure ignoring the loops. The     end result would be an enumerable which enumerates all the
        ///// items in the data structure regardless of the level of nesting.
        ///// </summary>
        ///// <typeparam name="T">Type of the recursive data structure</typeparam>
        ///// <param name="source">Source element</param>
        ///// <param name="childrenSelector">a function that returns the children of a     given data element of type T</param>
        ///// <param name="keySelector">a function that returns a key value for each   element</param>
        ///// <returns>a faltten list of all the items within recursive data structure of T</returns>
        //public static IEnumerable<T> Flatten<T>(this T source, Func<T, IEnumerable<T>> childrenSelector, Func<T, object> keySelector) where T : class
        //{
        //    return Flatten(new[] { source }, childrenSelector, keySelector);
        //}
    }
}
