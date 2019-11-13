using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Zek.Extensions.Caching.Memory
{
    public static class CacheExtensions
    {
        public static void Set(this IMemoryCache cache, object key, object content, int durationInSeconds, int? slidingInSeconds = null) => Set(cache, key, content, DateTimeOffset.Now + TimeSpan.FromSeconds(durationInSeconds), slidingInSeconds);
        public static void Set(this IMemoryCache cache, object key, object content, DateTimeOffset? absoluteExpiration = null, int? slidingInSeconds = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration ?? DateTime.Now + TimeSpan.FromSeconds(60),
                SlidingExpiration = slidingInSeconds != null ? TimeSpan.FromSeconds(slidingInSeconds.Value) : (TimeSpan?)null,
                //AbsoluteExpirationRelativeToNow = 
                Priority = CacheItemPriority.Low
            };

            cache.Set(key, content, options);
        }

        public static T TryGetValue<T>(this IMemoryCache cache, string key, T defaultValue = default(T))
        {
            if (cache.TryGetValue(key, out var result))
            {
                return (T)result;
            }

            return defaultValue;
        }

        public static async Task<TItem> GetOrCreateAsync<TItem>(this IMemoryCache cache, object key, Func<ICacheEntry, Task<TItem>> factory, bool bypass, int durationInSeconds, int? slidingInSeconds = null)
        {
            cache.RemoveIfBypass(key, bypass);
            if (!cache.TryGetValue(key, out var result))
            {
                var entry = cache.CreateEntry(key);

                //entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(durationInSeconds);
                entry.AbsoluteExpiration = DateTimeOffset.Now + TimeSpan.FromSeconds(durationInSeconds);

                if (slidingInSeconds != null)
                    entry.SlidingExpiration = TimeSpan.FromSeconds(slidingInSeconds.Value);

                result = await factory(entry);
                entry.SetValue(result);
                // need to manually call dispose instead of having a using
                // in case the factory passed in throws, in which case we
                // do not want to add the entry to the cache
                entry.Dispose();
            }

            return (TItem)result;
        }


        public static void RemoveIfBypass(this IMemoryCache cache, object key, bool bypass = true)
        {
            if (bypass)
                cache.Remove(key);
        }
    }
}
