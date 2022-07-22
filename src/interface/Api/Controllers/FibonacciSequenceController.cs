using Api.Models.Response;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Produces("application/json")]
[Route("v{version:apiVersion}/[controller]")]
public class FibonacciSequenceController : ControllerBase
{
    /// <summary>
    /// Search or calculate the corresponding value in the Fibonacci sequence
    /// </summary>
    /// <param name="number"> Input for search, the value must be a positive integer </param>

    [HttpGet("{number}")]
    [ProducesResponseType(typeof(FibonacciNumberResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetSequenceNumber([FromServices] IFibonacciSequenceService service, [FromRoute] uint number)
    {
        var responseNumber = await service.GetFibbonacciNumberAsync(number);
        return Ok(new FibonacciNumberResponse(responseNumber));
    }
}
