using Application.Services;

using Repository.Cache;

namespace Application.Test.Services;
public class FibonacciSequenceServiceTests
{
    private readonly Mock<ICacheRepository> _cache;
    private readonly FibonacciSequenceService _service;

    public FibonacciSequenceServiceTests()
    {
        _cache = new Mock<ICacheRepository>();
        _service = new FibonacciSequenceService(_cache.Object);
    }

    [Fact]
    public async Task GetFibonacciKey_BaseCase_ShouldReturnTheCorrectKey()
    {
        //Arrange
        //Act
        var fibNumber = await _service.GetFibbonacciNumberAsync(1);

        //Assert
        Assert.Equal("1", fibNumber);
        _cache.Verify(c => c.AddAsync<string>(It.IsAny<string>(), It.IsAny<string>(), null, null), Times.Never);
    }

    [Fact]
    public async Task GetFibonacciKey_WithoutCache_ShouldReturnTheCorrectKey()
    {
        //Arrange
        //Act
        var fibNumber = await _service.GetFibbonacciNumberAsync(5);

        //Assert
        Assert.Equal("5", fibNumber);
        _cache.Verify(c => c.GetAsync<string>("FIBONACCI:5"), Times.Once);
        _cache.Verify(c => c.GetAsync<string>("FIBONACCI:4"), Times.Once);
        _cache.Verify(c => c.GetAsync<string>("FIBONACCI:3"), Times.Exactly(2));
        _cache.Verify(c => c.GetAsync<string>("FIBONACCI:2"), Times.Exactly(3));
        _cache.Verify(c => c.AddAsync<string>("FIBONACCI:5", "5", null, null), Times.Once);
        _cache.Verify(c => c.AddAsync<string>("FIBONACCI:4", "3", null, null), Times.Once);
        _cache.Verify(c => c.AddAsync<string>("FIBONACCI:3", "2", null, null), Times.Exactly(2));
        _cache.Verify(c => c.AddAsync<string>("FIBONACCI:2", "1", null, null), Times.Exactly(3));
    }

    [Fact]
    public async Task GetFibonacciKey_WithCache_ShouldReturnTheCorrectKey()
    {
        //Arrange
        _cache.Setup(c => c.GetAsync<string>("FIBONACCI:5")).ReturnsAsync("5");
        //Act
        var fibNumber = await _service.GetFibbonacciNumberAsync(5);

        //Assert
        Assert.Equal("5", fibNumber);
        _cache.Verify(c => c.GetAsync<string>("FIBONACCI:5"), Times.Once);
        _cache.Verify(c => c.AddAsync<string>(It.IsAny<string>(), It.IsAny<string>(), null, null), Times.Never);
    }

    [Fact]
    public async Task GetFibonacciKey_WithPartialCache_ShouldReturnTheCorrectKey()
    {
        //Arrange
        _cache.Setup(c => c.GetAsync<string>("FIBONACCI:5")).ReturnsAsync("5");
        _cache.Setup(c => c.GetAsync<string>("FIBONACCI:6")).ReturnsAsync("8");

        //Act
        var fibNumber = await _service.GetFibbonacciNumberAsync(7);

        //Assert
        Assert.Equal("13", fibNumber);
        _cache.Verify(c => c.GetAsync<string>("FIBONACCI:5"), Times.Once);
        _cache.Verify(c => c.GetAsync<string>("FIBONACCI:6"), Times.Once);
        _cache.Verify(c => c.AddAsync<string>("FIBONACCI:7", "13", null, null), Times.Once);
    }
}