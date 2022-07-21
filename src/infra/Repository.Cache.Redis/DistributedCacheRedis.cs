using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Repository.Cache.Redis;
public class DistributedCacheRedis : ICacheRepository
{
    private readonly IDistributedCache _cache;

    public DistributedCacheRedis(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task AddAsync<T>(string key, T data, TimeSpan? absoluteExpirationTime = null, TimeSpan? slidingExpiration = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpirationTime,
            SlidingExpiration = slidingExpiration
        };

        var json = JsonSerializer.Serialize(data);

        await _cache.SetStringAsync(key, json, options);
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var json = await _cache.GetStringAsync(key);

        if (json is null)
            return default;

        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}