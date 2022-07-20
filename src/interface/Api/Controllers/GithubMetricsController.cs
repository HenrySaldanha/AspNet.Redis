using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Produces("application/json")]
[Route("v{version:apiVersion}/[controller]")]
public class GithubMetricsController : ControllerBase
{
    [HttpGet("latestrepositories/{userName}")]
    //[ProducesResponseType(typeof(type), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetLatestRepositories([FromRoute] string userName)
    {
        throw new NotImplementedException();
    }

    [HttpGet("moststarredrepository/{userName}")]
    //[ProducesResponseType(typeof(type), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMostStarredRepository([FromRoute] string userName)
    {
        //github api https://api.github.com/users/{userName}/repos
        throw new NotImplementedException();
    }
}