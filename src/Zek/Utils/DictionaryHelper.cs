using System.Collections.Generic;
using Zek.Extensions.Collections;

namespace Zek.Utils
{
    public static class DictionaryHelper
    {
        public static Dictionary<TKey, TValue> GetMerged<TKey, TValue>(IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            var merged = new Dictionary<TKey, TValue>();
            first.ForEach(kv => merged[kv.Key] = kv.Value);
            second.ForEach(kv => merged[kv.Key] = kv.Value);

            return merged;
        }
    }
}
