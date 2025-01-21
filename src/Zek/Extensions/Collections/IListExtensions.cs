namespace Zek.Extensions.Collections
{
    public static class IListExtensions
    {
        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="collection">The collection.</param>
        public static void RemoveRange<T>(this IList<T> items, IEnumerable<T> collection)
        {
            // Remove range from local items
            collection.ForEach(p => items.Remove(p));
        }
    }
}
