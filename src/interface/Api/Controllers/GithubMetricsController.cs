using Api.Models.Response;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Produces("application/json")]
[Route("v{version:apiVersion}/[controller]")]
public class GithubMetricsController : ControllerBase
{
    /// <summary>
    /// Get the 5 latest repositories (the cache is refreshed every 1 week)
    /// </summary>
    /// <param name="userName"> User name for search in github </param>
    [HttpGet("latestrepositories/{userName}")]
    [ProducesResponseType(typeof(IEnumerable<GitHubRepositoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetLatestRepositoriesAsync([FromServices] IGithubService service, [FromRoute] string userName)
    {
        var response = await service.GetLatestRepositoriesAsync(userName);

        if (!response.Any())
            return NotFound();

        return Ok(response.Select(c => (GitHubRepositoryResponse)c));
    }

    /// <summary>
    /// Get the 5 most starred repositories (the cache is refreshed every 1 week)
    /// </summary>
    /// <param name="userName"> User name for search in github </param>
    [HttpGet("moststarredrepository/{userName}")]
    [ProducesResponseType(typeof(IEnumerable<GitHubRepositoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMostStarredRepositoryAsync([FromServices] IGithubService service, [FromRoute] string userName)
    {
        var response = await service.GetMostStarredRepositoryAsync(userName);

        if (!response.Any())
            return NotFound();

        return Ok(response.Select(c => (GitHubRepositoryResponse)c));
    }
}