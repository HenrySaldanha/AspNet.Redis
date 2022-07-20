using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Produces("application/json")]
[Route("v{version:apiVersion}/[controller]")]
public class IndicatorsController : ControllerBase
{
    [HttpPost]
    //[ProducesResponseType(typeof(type), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateAsync(/*request*/)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{key}")]
    //[ProducesResponseType(typeof(type), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAsync([FromRoute] string key)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{key}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteAsync(/*request*/)
    {
        throw new NotImplementedException();
    }

}