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
    private readonly IFibonacciSequenceService _service;

    public FibonacciSequenceController(IFibonacciSequenceService service)
    {
        _service = service;
    }

    [HttpGet("{number}")]
    [ProducesResponseType(typeof(FibonacciNumberResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetSequenceNumber([FromRoute] uint number)
    {
        var responseNumber = await _service.GetFibbonacciNumberAsync(number);
        return Ok(new FibonacciNumberResponse(responseNumber));
    }
}
