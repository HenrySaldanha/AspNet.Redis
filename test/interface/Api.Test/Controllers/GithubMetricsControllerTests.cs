using Api.Controllers;
using Api.Models.Response;
using Application.IServices;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Test.Controllers;
public class GithubMetricsControllerTests
{
    private readonly GithubMetricsController _controller;
    private readonly Mock<IGithubService> _service;

    public GithubMetricsControllerTests()
    {
        _service = new Mock<IGithubService>();
        _controller = new GithubMetricsController();
    }

    [Fact]
    public async Task GetLatestRepositoriesAsync_MustBeOk()
    {
        //Arrange
        var user = "user";
        var repos = new List<GitHubRepository> { new GitHubRepository() };
        _service.Setup(c => c.GetLatestRepositoriesAsync(user)).ReturnsAsync(repos);

        //Act
        var response = await _controller.GetLatestRepositoriesAsync(_service.Object, user);

        //Assert
        Assert.IsType<OkObjectResult>(response);
        Assert.IsAssignableFrom<IEnumerable<GitHubRepositoryResponse>>(((ObjectResult)response).Value);
        _service.Verify(c => c.GetLatestRepositoriesAsync(user), Times.Once);
    }

    [Fact]
    public async Task GetLatestRepositoriesAsync_MustBeNotFound()
    {
        //Arrange
        var user = "user";
        _service.Setup(c => c.GetLatestRepositoriesAsync(user)).ReturnsAsync(new List<GitHubRepository>());

        //Act
        var response = await _controller.GetLatestRepositoriesAsync(_service.Object, user);

        //Assert
        Assert.IsType<NotFoundResult>(response);
        _service.Verify(c => c.GetLatestRepositoriesAsync(user), Times.Once);
    }

    [Fact]
    public async Task GetMostStarredRepositoryAsync_MustBeOk()
    {
        //Arrange
        var user = "user";
        var repos = new List<GitHubRepository> { new GitHubRepository() };
        _service.Setup(c => c.GetMostStarredRepositoryAsync(user)).ReturnsAsync(repos);

        //Act
        var response = await _controller.GetMostStarredRepositoryAsync(_service.Object, user);

        //Assert
        Assert.IsType<OkObjectResult>(response);
        Assert.IsAssignableFrom<IEnumerable<GitHubRepositoryResponse>>(((ObjectResult)response).Value);
        _service.Verify(c => c.GetMostStarredRepositoryAsync(user), Times.Once);
    }

    [Fact]
    public async Task GetMostStarredRepositoryAsync_MustBeNotFound()
    {
        //Arrange
        var user = "user";
        _service.Setup(c => c.GetMostStarredRepositoryAsync(user)).ReturnsAsync(new List<GitHubRepository>());

        //Act
        var response = await _controller.GetMostStarredRepositoryAsync(_service.Object, user);

        //Assert
        Assert.IsType<NotFoundResult>(response);
        _service.Verify(c => c.GetMostStarredRepositoryAsync(user), Times.Once);
    }
}