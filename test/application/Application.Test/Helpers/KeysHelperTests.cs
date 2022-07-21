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
}