﻿namespace Zek.Extensions.Collections.Recursive
{
    public static class RecursiveJoinExtensions
    {
        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> parentKeySelector, Func<TSource, TKey> childKeySelector, Func<TSource, IEnumerable<TResult>, TResult> resultSelector)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector, resultSelector, Comparer<TKey>.Default);
        }

        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> parentKeySelector, Func<TSource, TKey> childKeySelector, Func<TSource, int, IEnumerable<TResult>, TResult> resultSelector)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector, (TSource element, int depth, int index, IEnumerable<TResult> children) => resultSelector(element, index, children));
        }

        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> parentKeySelector, Func<TSource, TKey> childKeySelector, Func<TSource, IEnumerable<TResult>, TResult> resultSelector, IComparer<TKey> comparer)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector, (TSource element, int depth, int index, IEnumerable<TResult> children) => resultSelector(element, children), comparer);
        }

        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> parentKeySelector, Func<TSource, TKey> childKeySelector, Func<TSource, int, IEnumerable<TResult>, TResult> resultSelector, IComparer<TKey> comparer)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector, (TSource element, int depth, int index, IEnumerable<TResult> children) => resultSelector(element, index, children), comparer);
        }

        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> parentKeySelector, Func<TSource, TKey> childKeySelector, Func<TSource, int, int, IEnumerable<TResult>, TResult> resultSelector)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector, resultSelector, Comparer<TKey>.Default);
        }

        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> parentKeySelector, Func<TSource, TKey> childKeySelector, Func<TSource, int, int, IEnumerable<TResult>, TResult> resultSelector, IComparer<TKey> comparer)
        {
            // prevent source being enumerated more than once per RecursiveJoin call
            source = new LinkedList<TSource>(source);

            // fast binary search lookup
            var parents = new SortedDictionary<TKey, TSource>(comparer);
            var children = new SortedDictionary<TKey, LinkedList<TSource>>(comparer);

            foreach (var element in source)
            {
                parents[parentKeySelector(element)] = element;

                LinkedList<TSource> list;

                var childKey = childKeySelector(element);

                if (!children.TryGetValue(childKey, out list))
                {
                    children[childKey] = list = new LinkedList<TSource>();
                }

                list.AddLast(element);
            }

            // initialize to null otherwise compiler complains at single line assignment
            Func<TSource, int, IEnumerable<TResult>> childSelector = null;

            childSelector = (parent, depth) =>
            {

                if (children.TryGetValue(parentKeySelector(parent), out LinkedList<TSource> innerChildren))
                {
                    return innerChildren.Select((child, index) => resultSelector(child, index, depth, childSelector(child, depth + 1)));
                }

                return Enumerable.Empty<TResult>();
            };

            return source.Where(element => !parents.ContainsKey(childKeySelector(element))).Select((element, index) => resultSelector(element, index, 0, childSelector(element, 1)));
        }
    }
}
