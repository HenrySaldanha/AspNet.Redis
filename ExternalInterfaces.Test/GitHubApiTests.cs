using Domain;
using ExternalInterfaces.Model;
using Flurl.Http;
using Flurl.Http.Testing;
using Microsoft.Extensions.Options;

namespace ExternalInterfaces.Test;
public class GitHubApiTests
{
    private readonly GitHubApi _api;

    public GitHubApiTests()
    {
        var options = Options.Create(new GitHubApiOptions { GetRepositoriesUrl = "http://url/{0}/repos" });
        _api = new GitHubApi(options);
    }

    [Fact]
    public async Task GetRepositoriesByUserAsync_MustReturnSuccess()
    {
        //Arrange
        var user = "user";
        var url = "http://url/user/repos";

        var responseApi = new List<ApiRepositoryResponse> {
            new ApiRepositoryResponse
            {
                Created_at = DateTime.UtcNow,
                Description = "desc",
                Id = 12,
                Language = "c#",
                Name = "name",
                Stargazers_count = 33,
                Url = "http://aaa",
                Owner = new ApiOwnerResponse
                {
                    Url = "http://aaa",
                    Id = 12,
                    Login = "login"
                }
            }
        };

        //Act
        using var httpTest = new HttpTest();
        httpTest.ForCallsTo(url).RespondWithJson(responseApi, 200);

        var repos = await _api.GetRepositoriesByUserAsync(user);

        //Assert
        httpTest.ShouldHaveCalled(url).WithVerb(HttpMethod.Get);
        Assert.IsAssignableFrom<IEnumerable<GitHubRepository>>(repos);
    }

    [Fact]
    public async Task GetRepositoriesByUserAsync_MustReturnEmptyList()
    {
        //Arrange
        var user = "user";
        var url = "http://url/user/repos";

        //Act
        using var httpTest = new HttpTest();
        httpTest.ForCallsTo(url).RespondWithJson(null, 404);

        var repos = await _api.GetRepositoriesByUserAsync(user);

        //Assert
        httpTest.ShouldHaveCalled(url).WithVerb(HttpMethod.Get);
        Assert.IsAssignableFrom<IEnumerable<GitHubRepository>>(repos);
    }

    [Fact]
    public async Task GetRepositoriesByUser_MustReturnException()
    {
        //Arrange
        var user = "user";
        var url = "http://url/user/repos";


        //Act
        //Assert
        using var httpTest = new HttpTest();
        httpTest.ForCallsTo(url).SimulateException(new Exception("ex"));

        await Assert.ThrowsAsync<FlurlHttpException>(async () => {
            var repos = await _api.GetRepositoriesByUserAsync(user);
        });

        httpTest.ShouldHaveCalled(url).WithVerb(HttpMethod.Get);
    }
}