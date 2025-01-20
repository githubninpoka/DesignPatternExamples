using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CacheRedisBlazor.Extensions;

public static class DistributedCacheExtension
{
    public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data, TimeSpan? absoluteExpireTime= null,
        TimeSpan? unusedExpireTime = null)
    {
        var options = new DistributedCacheEntryOptions();
        options.SetAbsoluteExpiration(absoluteExpireTime?? TimeSpan.FromSeconds(60));
        options.SetSlidingExpiration(unusedExpireTime ?? TimeSpan.FromSeconds(60));

        var toTheCache = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(recordId, toTheCache,options);
    }

    public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
    {
        var jsonData = await cache.GetStringAsync(recordId);
        if (jsonData is null)
        {
            return default;
        }
        T data = JsonSerializer.Deserialize<T>(jsonData);
        return data;
    }
}
