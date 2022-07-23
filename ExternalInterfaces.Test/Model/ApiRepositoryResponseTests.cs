using Domain;
using ExternalInterfaces.Model;

namespace ExternalInterfaces.Test.Model;
public class ApiRepositoryResponseTests
{
    [Fact]
    public void ApiRepositoryResponse_ValidInput_MustReturnGitHubRepository()
    {
        //Arrange
        var request = new ApiRepositoryResponse
        {
            Created_at = DateTime.Now,
            Description = "Description",
            Id = 123,
            Language = "C#",
            Name = "Name",
            Url = "http://aaaa",
            Stargazers_count = 123,
            Owner = new ApiOwnerResponse
            {
                Id = 123,
                Login = "name",
                Url = "http://aaa"
            }
        };

        //Act
        var repo = (GitHubRepository)request;

        //Assert
        Assert.NotNull(repo);
        Assert.Equal(repo.CreateTime, request.Created_at);
        Assert.Equal(repo.Description, request.Description);
        Assert.Equal(repo.Id, request.Id);
        Assert.Equal(repo.Language, request.Language);
        Assert.Equal(repo.Name, request.Name);
        Assert.Equal(repo.Url, request.Url);
        Assert.Equal(repo.Stars, (uint)request.Stargazers_count);
        Assert.Equal(repo.Owner.Id, request.Owner.Id);
        Assert.Equal(repo.Owner.LoginName, request.Owner.Login);
        Assert.Equal(repo.Owner.Url, request.Owner.Url);
    }


    [Fact]
    public void ApiRepositoryResponse_InvalidInput_MustReturnNull()
    {

        //Arrange
        ApiRepositoryResponse request = null;

        //Act
        var repo = (GitHubRepository)request;

        //Assert
        Assert.Null(repo);
    }

    [Fact]
    public void ApiRepositoryResponse_OwnerNull_MustReturnOwnerNull()
    {

        //Arrange
        var request = new ApiRepositoryResponse
        {
            Created_at = DateTime.Now,
            Description = "Description",
            Id = 123,
            Language = "C#",
            Name = "Name",
            Url = "http://aaaa",
            Stargazers_count = 123,
            Owner = null
        };

        //Act
        var repo = (GitHubRepository)request;

        //Assert
        Assert.NotNull(repo);
        Assert.Null(repo.Owner);
        Assert.Equal(repo.CreateTime, request.Created_at);
        Assert.Equal(repo.Description, request.Description);
        Assert.Equal(repo.Id, request.Id);
        Assert.Equal(repo.Language, request.Language);
        Assert.Equal(repo.Name, request.Name);
        Assert.Equal(repo.Url, request.Url);
        Assert.Equal(repo.Stars, (uint)request.Stargazers_count);
    }
}