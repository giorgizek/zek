namespace Zek.Extensions.Collections
{
    public static class DictionaryExtensions
    {
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
    }
}
