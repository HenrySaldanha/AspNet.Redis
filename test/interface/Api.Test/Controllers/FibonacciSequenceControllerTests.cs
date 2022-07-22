using Api.Controllers;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Test.Controllers;
public class FibonacciSequenceControllerTests
{
    private readonly FibonacciSequenceController _controller;
    private readonly Mock<IFibonacciSequenceService> _service;

    public FibonacciSequenceControllerTests()
    {
        _service = new Mock<IFibonacciSequenceService>();
        _controller = new FibonacciSequenceController();
    }

    [Fact]
    public async Task GetSequenceNumberAsync_MustBeSuccess()
    {
        //Arrange
        uint number = 5;

        //Act
        var response = await _controller.GetSequenceNumberAsync(_service.Object, number);

        //Assert
        Assert.IsType<OkObjectResult>(response);
        _service.Verify(c => c.GetFibbonacciNumberAsync(number), Times.Once);
    }
}