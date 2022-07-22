using Domain;

namespace ExternalInterfaces.Model;

public class ApiRepositoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ApiOwnerResponse Owner { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public DateTime Created_at { get; set; }
    public int Stargazers_count { get; set; }
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
    public string Login { get; set; }
    public int Id { get; set; }
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