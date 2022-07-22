using Api.Controllers;
using Api.Models.Request;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Test.Controllers;
public class IndicatorsControllerTests
{
    private readonly IndicatorsController _controller;
    private readonly Mock<IIndicatorsService> _service;

    public IndicatorsControllerTests()
    {
        _service = new Mock<IIndicatorsService>();
        _controller = new IndicatorsController(_service.Object);
    }

    [Fact]
    public async Task GetAsync_MustBeOk()
    {
        //Arrange
        var key = "key";
        _service.Setup(c => c.GetAsync(key)).ReturnsAsync("value");

        //Act
        var response = await _controller.GetAsync(key);

        //Assert
        Assert.IsType<OkObjectResult>(response);
        _service.Verify(c => c.GetAsync(key), Times.Once);
    }

    [Fact]
    public async Task GetAsync_MustBeNotFound()
    {
        //Arrange
        var key = "key";
        _service.Setup(c => c.GetAsync(key)).ReturnsAsync((string)null);

        //Act
        var response = await _controller.GetAsync(key);

        //Assert
        Assert.IsType<NotFoundResult>(response);
        _service.Verify(c => c.GetAsync(key), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_MustBeNoContent()
    {
        //Arrange
        var key = "key";

        //Act
        var response = await _controller.DeleteAsync(key);

        //Assert
        Assert.IsType<NoContentResult>(response);
        _service.Verify(c => c.DeleteAsync(key), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_MustBeCreated()
    {
        //Arrange
        var request = new IndicatorRequest { Key = "key", Value = "value" };

        //Act
        var response = await _controller.InsertOrUpdateAsync(request);

        //Assert
        Assert.IsType<CreatedResult>(response);
        _service.Verify(c => c.InsertOrUpdateAsync(request.Key, request.Value), Times.Once);
    }
}