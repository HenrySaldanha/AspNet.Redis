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

        var repositories = _gitHubApi.GetRepositoriesByUser(userName);


        //TODO: implement this
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<GitHubRepository>> GetMostStarredRepositoryAsync(string userName)
    {
        Log.Information("Service: {0} Method: {1} Request: {2}",
          nameof(GithubService), nameof(GetMostStarredRepositoryAsync), userName);

        var key = KeysHelper.GetMostStarredRepositoryKey(userName);

        var cacheData = await _cache.GetAsync<IEnumerable<GitHubRepository>>(key);

        if (cacheData is not null)
            return cacheData;

        //TODO: implement this
        throw new NotImplementedException();
    }
}