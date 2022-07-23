using Domain;

namespace ExternalInterfaces;
public interface IGitHubApi
{
    public Task<IEnumerable<GitHubRepository>> GetRepositoriesByUserAsync(string userName);
}