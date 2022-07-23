using Application.Services;
using Domain;
using ExternalInterfaces;
using Moq;
using Repository.Cache;

namespace Application.Test.Services
{
    public class GithubServiceTests
    {
        private readonly Mock<ICacheRepository> _cache;
        private readonly Mock<IGitHubApi> _api;
        private readonly GithubService _service;

        public GithubServiceTests()
        {
            _cache = new Mock<ICacheRepository>();
            _api = new Mock<IGitHubApi>();
            _service = new GithubService(_cache.Object, _api.Object);
        }

        [Fact]
        public async Task GetLatestRepositoriesAsync_HasCache_ShouldReturnRepositories()
        {
            //Arrange
            string username = "username";
            var key = "GITHUB_LATEST_REPOS:username";
            var repos = new List<GitHubRepository>
            {
                new GitHubRepository()
            };
            _cache.Setup(c => c.GetAsync<IEnumerable<GitHubRepository>>(key)).ReturnsAsync(repos);

            //Act
            var response = await _service.GetLatestRepositoriesAsync(username);

            //Assert
            Assert.Equal(response, repos);
            _cache.Verify(c => c.GetAsync<IEnumerable<GitHubRepository>>(key), Times.Once);
        }


        [Fact]
        public async Task GetLatestRepositoriesAsync_ApiReturnsEmptylist_ShouldReturnEmptylist()
        {
            //Arrange
            string username = "username";
            var key = "GITHUB_LATEST_REPOS:username";
            _cache.Setup(c => c.GetAsync<IEnumerable<GitHubRepository>>(key)).ReturnsAsync((IEnumerable<GitHubRepository>)null);
            _api.Setup(c => c.GetRepositoriesByUserAsync(username)).ReturnsAsync(Enumerable.Empty<GitHubRepository>);

            //Act
            var response = await _service.GetLatestRepositoriesAsync(username);

            //Assert
            Assert.Empty(response);
            _cache.Verify(c => c.GetAsync<IEnumerable<GitHubRepository>>(key), Times.Once);
            _api.Verify(c => c.GetRepositoriesByUserAsync(username), Times.Once);
        }

        [Fact]
        public async Task GetLatestRepositoriesAsync_ApiReturnsValidList_ShouldReturnRepos()
        {
            //Arrange
            string username = "username";
            var key = "GITHUB_LATEST_REPOS:username";
            var repos = new List<GitHubRepository>
            {
                new GitHubRepository{ Id = 1, CreateTime = DateTime.Now.AddDays(-6)  },
                new GitHubRepository{ Id = 5, CreateTime = DateTime.Now },
            };
            _cache.Setup(c => c.GetAsync<IEnumerable<GitHubRepository>>(key)).ReturnsAsync((IEnumerable<GitHubRepository>)null);
            _api.Setup(c => c.GetRepositoriesByUserAsync(username)).ReturnsAsync(repos);

            //Act
            var response = await _service.GetLatestRepositoriesAsync(username);

            //Assert
            Assert.Equal(response.ToList()[0], repos[1]);
            Assert.Equal(response.ToList()[1], repos[0]);
            _cache.Verify(c => c.GetAsync<IEnumerable<GitHubRepository>>(key), Times.Once);
            _api.Verify(c => c.GetRepositoriesByUserAsync(username), Times.Once);
        }

        [Fact]
        public async Task GetMostStarredRepositoryAsync_HasCache_ShouldReturnRepositories()
        {
            //Arrange
            string username = "username";
            var key = "GITHUB_MOST_STAR_REPOS:username";
            var repos = new List<GitHubRepository>
            {
                new GitHubRepository()
            };
            _cache.Setup(c => c.GetAsync<IEnumerable<GitHubRepository>>(key)).ReturnsAsync(repos);

            //Act
            var response = await _service.GetMostStarredRepositoryAsync(username);

            //Assert
            Assert.Equal(response, repos);
            _cache.Verify(c => c.GetAsync<IEnumerable<GitHubRepository>>(key), Times.Once);
        }


        [Fact]
        public async Task GetMostStarredRepositoryAsync_ApiReturnsEmptylist_ShouldReturnEmptylist()
        {
            //Arrange
            string username = "username";
            var key = "GITHUB_MOST_STAR_REPOS:username";
            _cache.Setup(c => c.GetAsync<IEnumerable<GitHubRepository>>(key)).ReturnsAsync((IEnumerable<GitHubRepository>)null);
            _api.Setup(c => c.GetRepositoriesByUserAsync(username)).ReturnsAsync(Enumerable.Empty<GitHubRepository>);

            //Act
            var response = await _service.GetMostStarredRepositoryAsync(username);

            //Assert
            Assert.Empty(response);
            _cache.Verify(c => c.GetAsync<IEnumerable<GitHubRepository>>(key), Times.Once);
            _api.Verify(c => c.GetRepositoriesByUserAsync(username), Times.Once);
        }

        [Fact]
        public async Task GetMostStarredRepositoryAsync_ApiReturnsValidList_ShouldReturnRepos()
        {
            //Arrange
            string username = "username";
            var key = "GITHUB_MOST_STAR_REPOS:username";
            var repos = new List<GitHubRepository>
            {
                new GitHubRepository{ Id = 1, Stars = 3  },
                new GitHubRepository{ Id = 5, Stars = 6 },
            };
            _cache.Setup(c => c.GetAsync<IEnumerable<GitHubRepository>>(key)).ReturnsAsync((IEnumerable<GitHubRepository>)null);
            _api.Setup(c => c.GetRepositoriesByUserAsync(username)).ReturnsAsync(repos);

            //Act
            var response = await _service.GetMostStarredRepositoryAsync(username);

            //Assert
            Assert.Equal(response.ToList()[0], repos[1]);
            Assert.Equal(response.ToList()[1], repos[0]);
            _cache.Verify(c => c.GetAsync<IEnumerable<GitHubRepository>>(key), Times.Once);
            _api.Verify(c => c.GetRepositoriesByUserAsync(username), Times.Once);
        }

    }
}
