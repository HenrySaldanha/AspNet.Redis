using Application.Helpers;
using Application.IServices;
using Repository.Cache;
using Serilog;
using System.Numerics;

namespace Application.Services;
public class FibonacciSequenceService : IFibonacciSequenceService
{
    private readonly ICacheRepository _cache;

    public FibonacciSequenceService(ICacheRepository cache)
    {
        _cache = cache;
    }

    public async Task<string> GetFibbonacciNumberAsync(uint n)
    {
        Log.Information("Service: {0} Method: {1} Request: {2}",
            nameof(FibonacciSequenceService), nameof(GetFibbonacciNumberAsync), n);

        if (n <= 1)
            return n.ToString();

        var key = KeysHelper.GetFibonacciKey(n);
        var cacheNumber = await _cache.GetAsync<string>(key);

        if (cacheNumber != null)
            return cacheNumber;

        var prevNumber = BigInteger.Parse(await GetFibbonacciNumberAsync(n - 1));
        var prevPrevNumber = BigInteger.Parse(await GetFibbonacciNumberAsync(n - 2));

        var sum = (prevNumber + prevPrevNumber).ToString();
        await _cache.AddAsync(key, sum);

        return sum;
    }
}