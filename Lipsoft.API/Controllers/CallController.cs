using Lipsoft.API.Dtos.Requests.Call;
using Lipsoft.BLL.Errors;
using Lipsoft.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lipsoft.API.Controllers;

[ApiController]
[Route("calls")]
public class CallController(ICallService callService) : ControllerBase
{
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    public async Task<IActionResult> GetCall(long id, CancellationToken cancellationToken)
    {
        var result = await callService.GetCallByIdAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                NotFoundError => NotFound(result.Error.Message), 
                _ => BadRequest(result.Error?.Message) 
            };
        }
    
        return Ok(result.Value); 
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> GetAllCalls(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await callService.GetAllCallsAsync(pageNumber, pageSize, cancellationToken);
    
        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                ValidationError => BadRequest(result.Error.Message), 
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message) 
            };
        }

        return Ok(new
        {
            Calls = result.Value.Calls,
            TotalCount = result.Value.TotalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        }); 
    }
    
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> AddCall([FromBody] AddCallDto addCallDto, CancellationToken cancellationToken)
    {
        var call = addCallDto.ToCall();
    
        var result = await callService.AddCallAsync(call, cancellationToken);
    
        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                ValidationError => BadRequest(result.Error.Message), 
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message) 
            };
        }

        return Ok(result.Value);
    }
    
    [HttpPut("update/{id:long}")] 
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> UpdateCall(long id, [FromBody] UpdateCallDto updateCallDto, CancellationToken cancellationToken)
    {
        var call = updateCallDto.ToCall(id);
    
        var result = await callService.UpdateCallAsync(call, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                NotFoundError => NotFound(result.Error.Message), 
                ValidationError => BadRequest(result.Error.Message), 
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message) 
            };
        }

        return Ok(result.Value); 
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCall(long id, CancellationToken cancellationToken)
    {
        var result = await callService.DeleteCallAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                NotFoundError => NotFound(result.Error.Message),
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message) 
            };
        }

        return NoContent();
    }
}