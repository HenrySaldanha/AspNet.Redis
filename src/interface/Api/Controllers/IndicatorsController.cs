using Api.Models.Request;
using Api.Models.Response;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Produces("application/json")]
[Route("v{version:apiVersion}/[controller]")]
public class IndicatorsController : ControllerBase
{
    private readonly IIndicatorsService _service;

    public IndicatorsController(IIndicatorsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Registers or update a indicator in the cache with validity of 1 day
    /// </summary>
    /// <param name="request"> Key and value for the indicator </param>
    [HttpPost]
    [ProducesResponseType(typeof(IndicatorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> InsertOrUpdateAsync([FromBody] IndicatorRequest request)
    {
        var response = await _service.InsertOrUpdateAsync(request.Key, request.Value);
        return Created(nameof(InsertOrUpdateAsync), new IndicatorResponse(response));
    }

    /// <summary>
    /// Get a registered indicator in the cache
    /// </summary>
    /// <param name="key"> Indicator key </param>
    [HttpGet("{key}")]
    [ProducesResponseType(typeof(IndicatorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAsync([FromRoute] string key)
    {
        var value = await _service.GetAsync(key);

        if (value is null)
            return NotFound();

        return Ok(new IndicatorResponse((key, value)));
    }

    /// <summary>
    /// Remove a registered indicator in the cache
    /// </summary>
    /// <param name="key"> Indicator key </param>
    [HttpDelete("{key}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteAsync([FromRoute] string key)
    {
        await _service.DeleteAsync(key);
        return NoContent();
    }
}