using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Repository.Cache.Redis.Test;
public class DistributedCacheRedisTests
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheRedis _redis;

    public DistributedCacheRedisTests()
    {
        var opts = Options.Create(new MemoryDistributedCacheOptions());
        _cache = new MemoryDistributedCache(opts);
        _redis = new DistributedCacheRedis(_cache);
    }

    [Fact]
    public async Task AddAsync_MustBeSaveData()
    {
        //Arrange
        var key = "key";
        var data = 123;
        var jsonData = JsonSerializer.Serialize(data);

        //Act
        await _redis.AddAsync<int>(key, data, TimeSpan.FromMinutes(10), null);
        var cacheData = Encoding.UTF8.GetString(_cache.Get(key));

        //Assert
        Assert.Equal(jsonData, cacheData);
    }

    [Fact]
    public async Task GetAsync_MustBeGetData()
    {
        //Arrange
        var key = "key";
        var data = 123;
        _cache.SetString(key, JsonSerializer.Serialize(data));

        //Act
        var responseData = await _redis.GetAsync<int>(key);

        //Assert
        Assert.Equal(data, responseData);
    }

    [Fact]
    public async Task RemoveAsync_MustBeRemoveKeyAndData()
    {
        //Arrange
        var key = "key";
        var data = 123;
        _cache.SetString(key, JsonSerializer.Serialize(data));

        //Act
        await _redis.RemoveAsync(key);
        var cacheData = _cache.Get(key);

        //Assert
        Assert.Null(cacheData);
    }
}
