using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Produces("application/json")]
[Route("v{version:apiVersion}/[controller]")]
public class FibonacciSequenceController : ControllerBase
{

    [HttpGet("{number}")]
    //[ProducesResponseType(typeof(type), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetSequenceNumber([FromRoute] int number)
    {
        throw new NotImplementedException();
    }
}
