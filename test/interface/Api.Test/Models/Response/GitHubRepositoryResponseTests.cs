using Api.Models.Response;
using Domain;

namespace Api.Test.Models.Response;
public class GitHubRepositoryResponseTests
{
    [Fact]
    public void GitHubRepositoryResponse_ValidInput_MustReturnGitHubRepositoryResponse()
    {
        //Arrange
        var request = new GitHubRepository
        {
            CreateTime = DateTime.Now,
            Description = "Description",
            Id = 123,
            Language = "C#",
            Name = "Name",
            Url = "http://aaaa",
            Stars = 123,
            Owner = new RepositoryOwner
            {
                Id = 123,
                LoginName = "name",
                Url = "http://aaa"
            }
        };

        //Act
        var repo = (GitHubRepositoryResponse)request;

        //Assert
        Assert.NotNull(repo);
        Assert.Equal(repo.CreateTime, request.CreateTime);
        Assert.Equal(repo.Description, request.Description);
        Assert.Equal(repo.Id, request.Id);
        Assert.Equal(repo.Language, request.Language);
        Assert.Equal(repo.Name, request.Name);
        Assert.Equal(repo.Url, request.Url);
        Assert.Equal(repo.Stars, request.Stars);
        Assert.Equal(repo.Owner.Id, request.Owner.Id);
        Assert.Equal(repo.Owner.LoginName, request.Owner.LoginName);
        Assert.Equal(repo.Owner.Url, request.Owner.Url);
    }


    [Fact]
    public void GitHubRepositoryResponse_InvalidInput_MustReturnNull()
    {

        //Arrange
        GitHubRepository request = null;

        //Act
        var repo = (GitHubRepositoryResponse)request;

        //Assert
        Assert.Null(repo);
    }

    [Fact]
    public void GitHubRepositoryResponse_OwnerNull_MustReturnOwnerNull()
    {

        //Arrange
        var request = new GitHubRepository
        {
            CreateTime = DateTime.Now,
            Description = "Description",
            Id = 123,
            Language = "C#",
            Name = "Name",
            Url = "http://aaaa",
            Stars = 123,
            Owner = null
        };

        //Act
        var repo = (GitHubRepositoryResponse)request;

        //Assert
        Assert.NotNull(repo);
        Assert.Null(repo.Owner);
        Assert.Equal(repo.CreateTime, request.CreateTime);
        Assert.Equal(repo.Description, request.Description);
        Assert.Equal(repo.Id, request.Id);
        Assert.Equal(repo.Language, request.Language);
        Assert.Equal(repo.Name, request.Name);
        Assert.Equal(repo.Url, request.Url);
        Assert.Equal(repo.Stars, request.Stars);
    }
}