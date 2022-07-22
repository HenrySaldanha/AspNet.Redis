using Domain;
using ExternalInterfaces.Model;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net;

namespace ExternalInterfaces;
public class GitHubApi
{
    private readonly GitHubApiOptions _options;

    public GitHubApi(IOptions<GitHubApiOptions> options)
    {
        _options = options.Value;
    }

    public async Task<IEnumerable<GitHubRepository>> GetRepositoriesByUser(string userName)
    {
        Log.Debug("Service: {0} Method: {1} Request: {2}",
            nameof(GitHubApi), nameof(GetRepositoriesByUser), userName);

        var url = string.Format(_options.GetRepositoriesUrl, userName);

        try
        {
            var apiResponse = await url
                .WithHeader("User-Agent", "MyRedisApp")
                .AllowAnyHttpStatus()
                .GetAsync();

            if (apiResponse.StatusCode == (int)HttpStatusCode.OK)
            {
                var repos = await apiResponse.GetJsonAsync<IEnumerable<ApiRepositoryResponse>>();
                return repos.Select(c => (GitHubRepository)c);
            }

            return Enumerable.Empty<GitHubRepository>();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error: {0}", ex.Message);
            throw;
        }
    }
}