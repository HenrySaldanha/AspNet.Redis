using Application.Helpers;

namespace Application.Test.Helpers;
public class KeysHelperTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(99)]
    [InlineData(250)]
    public void GetFibonacciKey_ShouldReturnTheCorrectKey(uint n)
    {
        //Arrange
        //Act
        var key = KeysHelper.GetFibonacciKey(n);

        //Assert
        Assert.Equal($"FIBONACCI:{n}", key);
    }

    [Fact]
    public void GetLatestRepositoriesKey_ShouldReturnTheCorrectKey()
    {
        //Arrange
        var user = "test";

        //Act
        var key = KeysHelper.GetLatestRepositoriesKey(user);

        //Assert
        Assert.Equal($"GITHUB_LATEST_REPOS:{user}", key);
    }

    [Fact]
    public void GetMostStarredRepositoryKey_ShouldReturnTheCorrectKey()
    {
        //Arrange
        var user = "test";

        //Act
        var key = KeysHelper.GetMostStarredRepositoryKey(user);

        //Assert
        Assert.Equal($"GITHUB_MOST_STAR_REPOS:{user}", key);
    }
}