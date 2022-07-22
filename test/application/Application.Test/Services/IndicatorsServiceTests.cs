using Application.Services;
using Moq;
using Repository.Cache;

namespace Application.Test.Services;
public class IndicatorsServiceTests
{
    private readonly Mock<ICacheRepository> _cache;
    private readonly IndicatorsService _service;

    public IndicatorsServiceTests()
    {
        _cache = new Mock<ICacheRepository>();
        _service = new IndicatorsService(_cache.Object);
    }

    [Fact]
    public async Task CreateAsync_MustBeSuccess()
    {
        //Arrange
        var key = "key";
        var value = "value";

        //Act
        var itemCreated = await _service.CreateAsync(key, value);

        //Assert
        Assert.Equal((key,value), itemCreated);
        _cache.Verify(c => c.AddAsync(key, value, TimeSpan.FromDays(1), null), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException()
    {
        //Arrange
        var key = "key";
        var value = "value";
        _cache.Setup(c => c.AddAsync(key, value, TimeSpan.FromDays(1), null)).Throws<Exception>();

        //Act
        //Assert
        await Assert.ThrowsAsync<Exception>(async () => {
            await _service.CreateAsync(key, value);
        });
        _cache.Verify(c => c.AddAsync(key, value, TimeSpan.FromDays(1), null), Times.Once);
    }

    [Fact]
    public async Task GetAsync_MustBeSuccess()
    {
        //Arrange
        var key = "key";

        //Act
        await _service.GetAsync(key);

        //Assert
        _cache.Verify(c => c.GetAsync<string>(key), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_MustBeSuccess()
    {
        //Arrange
        var key = "key";

        //Act
        await _service.DeleteAsync(key);

        //Assert
        _cache.Verify(c => c.RemoveAsync(key), Times.Once);
    }

}