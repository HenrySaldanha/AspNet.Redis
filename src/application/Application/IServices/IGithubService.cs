using Domain;

namespace Application.IServices;
public interface IGithubService
{
    public Task<IEnumerable<GitHubRepository>> GetLatestRepositoriesAsync(string userName);
    public Task<IEnumerable<GitHubRepository>> GetMostStarredRepositoryAsync(string userName);
}