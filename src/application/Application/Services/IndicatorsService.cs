using Application.IServices;
using Repository.Cache;
using Serilog;

namespace Application.Services;
public class IndicatorsService : IIndicatorsService
{
    private readonly ICacheRepository _cache;

    public IndicatorsService(ICacheRepository cache)
    {
        _cache = cache;
    }

    public async Task<(string, string)> CreateAsync(string key, string value)
    {
        Log.Information("Service: {0} Method: {1} Request: {2}",
            nameof(IndicatorsService), nameof(CreateAsync), new { key, value });

        try
        {
            await _cache.AddAsync(key, value, TimeSpan.FromDays(1));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error: {0}", ex.Message);
            throw;
        }
        return (key, value);
    }

    public async Task DeleteAsync(string key)
    {
        Log.Information("Service: {0} Method: {1} Request: {2}",
            nameof(IndicatorsService), nameof(DeleteAsync), key);

        await _cache.RemoveAsync(key);
    }

    public async Task<string> GetAsync(string key)
    {
        Log.Information("Service: {0} Method: {1} Request: {2}",
            nameof(IndicatorsService), nameof(GetAsync), key);

        return await _cache.GetAsync<string>(key);
    }
}
