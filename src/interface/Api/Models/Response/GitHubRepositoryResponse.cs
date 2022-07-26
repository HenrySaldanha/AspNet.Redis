﻿using Domain;

namespace Api.Models.Response;

public class GitHubRepositoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public string Language { get; set; }
    public uint Stars { get; set; }
    public DateTime CreateTime { get; set; }
    public RepositoryOwnerResponse Owner { get; set; }

    public static implicit operator GitHubRepositoryResponse(GitHubRepository repo)
    {
        if (repo is null)
            return null;

        return new GitHubRepositoryResponse
        {
            CreateTime = repo.CreateTime,
            Name = repo.Name,
            Description = repo.Description,
            Url = repo.Url,
            Language = repo.Language,
            Stars = repo.Stars,
            Id = repo.Id,
            Owner = (RepositoryOwnerResponse)repo.Owner,
        };
    }
}
public class RepositoryOwnerResponse
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string LoginName { get; set; }

    public static implicit operator RepositoryOwnerResponse(RepositoryOwner repo)
    {
        if (repo is null)
            return null;

        return new RepositoryOwnerResponse
        {
            Id = repo.Id,
            LoginName = repo.LoginName,
            Url = repo.Url
        };
    }
}