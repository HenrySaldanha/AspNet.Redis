using Application.IServices;
using Application.Options;
using Repository.Cache;

namespace Application.Services;
public class FibonacciSequenceService : IFibonacciSequenceService
{
    private readonly ICacheRepository _cache;

    public FibonacciSequenceService(ICacheRepository cache)
    {
        _cache = cache;
    }

    public async Task<long> GetFibbonacciNumberAsync(int n)
    {
        if (n <= 1)
            return n;

        var key = KeysHelper.GetFibonacciKey(n);
        var cacheNumber = await _cache.GetAsync<long?>(key);

        if (cacheNumber.HasValue)
            return cacheNumber.Value;

        cacheNumber = await GetFibbonacciNumberAsync(n - 1) + await GetFibbonacciNumberAsync(n - 2);
        await _cache.AddAsync(key, cacheNumber.Value);

        return cacheNumber.Value;
    }
}