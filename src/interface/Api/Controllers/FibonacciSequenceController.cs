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
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<long> GetSequenceNumber([FromRoute] int number)
    {
        //TODO: Json Response
        return await _service.GetFibbonacciNumberAsync(number);
    }
}
