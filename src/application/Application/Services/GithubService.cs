using Application.Helpers;
using Application.IServices;
using Domain;
using ExternalInterfaces;
using Repository.Cache;
using Serilog;

namespace Application.Services;
public class GithubService : IGithubService
{
    private readonly ICacheRepository _cache;
    private readonly GitHubApi _gitHubApi;

    public GithubService(ICacheRepository cache)
    {
        _cache = cache;
        _gitHubApi = new GitHubApi();
    }

    public async Task<IEnumerable<GitHubRepository>> GetLatestRepositoriesAsync(string userName)
    {
        Log.Information("Service: {0} Method: {1} Request: {2}",
            nameof(GithubService), nameof(GetLatestRepositoriesAsync), userName);

        var key = KeysHelper.GetLatestRepositoriesKey(userName);
        var cacheData = await _cache.GetAsync<IEnumerable<GitHubRepository>>(key);

        if (cacheData is not null)
            return cacheData;

        var expireTime = TimeSpan.FromDays(7);
        var repositories = await _gitHubApi.GetRepositoriesByUser(userName);

        if (!repositories.Any())
            return repositories;

        repositories = repositories.OrderByDescending(c => c.CreateTime).Take(5);

        await _cache.AddAsync(key, repositories, expireTime);

        return repositories;
    }

    public async Task<IEnumerable<GitHubRepository>> GetMostStarredRepositoryAsync(string userName)
    {
        Log.Information("Service: {0} Method: {1} Request: {2}",
          nameof(GithubService), nameof(GetMostStarredRepositoryAsync), userName);

        var key = KeysHelper.GetMostStarredRepositoryKey(userName);
        var cacheData = await _cache.GetAsync<IEnumerable<GitHubRepository>>(key);

        if (cacheData is not null)
            return cacheData;

        var expireTime = TimeSpan.FromDays(7);
        var repositories = await _gitHubApi.GetRepositoriesByUser(userName);
        repositories = repositories.OrderByDescending(c => c.Stars).Take(5);

        if (!repositories.Any())
            return repositories;

        await _cache.AddAsync(key, repositories, expireTime);

        return repositories;
    }
}