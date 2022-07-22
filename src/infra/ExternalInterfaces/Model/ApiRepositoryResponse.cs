using Domain;
using System.Text.Json.Serialization;

namespace ExternalInterfaces.Model;

public class ApiRepositoryResponse
{
    //[JsonPropertyName("id")]
    public int Id { get; set; }

    //[JsonPropertyName("name")]
    public string Name { get; set; }

    //[JsonPropertyName("owner")]
    public ApiOwnerResponse Owner { get; set; }

    //[JsonPropertyName("description")]
    public string Description { get; set; }

    //[JsonPropertyName("url")]
    public string Url { get; set; }

    //[JsonPropertyName("created_at")]
    public DateTime Created_at { get; set; }

    //[JsonPropertyName("stargazers_count")]
    public int Stargazers_count { get; set; }

    //[JsonPropertyName("language")]
    public string Language { get; set; }

    public static implicit operator GitHubRepository(ApiRepositoryResponse apiModel)
    {
        if (apiModel is null)
            return null;

        return new GitHubRepository
        {
            CreateTime = apiModel.Created_at,
            Id = apiModel.Id,
            Description = apiModel.Description,
            Language = apiModel.Language,
            Name = apiModel.Name,
            Stars = (uint)apiModel.Stargazers_count,
            Url = apiModel.Url,
            Owner = (RepositoryOwner)apiModel.Owner
        };
    }
}

public class ApiOwnerResponse
{
    //[JsonPropertyName("login")]
    public string Login { get; set; }
    //[JsonPropertyName("id")]
    public int Id { get; set; }
    //[JsonPropertyName("url")]
    public string Url { get; set; }

    public static implicit operator RepositoryOwner(ApiOwnerResponse owner)
    {
        if (owner is null)
            return null;

        return new RepositoryOwner
        {
            Id = owner.Id,
            Url = owner.Url,
            LoginName = owner.Login
        };
    }
}