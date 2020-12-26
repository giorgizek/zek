using System;
using System.Collections.Generic;
using System.Linq;

namespace Zek.Extensions.Collections
{
    public static class DictionaryExtensions
    {
        public static TKey FindKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            foreach (var item in dictionary)
            {
                if (item.Value.Equals(value))
                    return item.Key;
            }

            return default(TKey);
        }

        public static TKey FindKey<TKey>(this IDictionary<TKey, string> dictionary, string value, StringComparison comparisonType)
        {
            foreach (var item in dictionary)
            {
                if (item.Value.Equals(value, comparisonType))
                    return item.Key;
            }

            return default(TKey);
        }

        
        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> instance, IDictionary<TKey, TValue> dictionary, bool replaceExisting = false)
        {
            foreach (var pair in dictionary)
            {
                if (replaceExisting || !instance.ContainsKey(pair.Key))
                {
                    instance[pair.Key] = pair.Value;
                }
            }
        }

        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            dictionary.TryGetValue(key, out var value);
            return value;
        }



        public delegate bool Predicate<TKey, TValue>(KeyValuePair<TKey, TValue> d);
        /// <summary>
        /// Remove an item from the collection with predicate
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void RemoveWhere<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Predicate<TKey, TValue> predicate)
        {
            foreach (var value in dictionary.ToArray().Where(value => predicate(value)))
                dictionary.Remove(value.Key);
        }
        
    }
}
