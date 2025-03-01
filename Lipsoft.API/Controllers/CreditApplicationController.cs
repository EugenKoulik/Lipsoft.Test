using Lipsoft.API.Dtos.Requests.CreditApplication;
using Lipsoft.BLL.Infrastructure.Errors;
using Lipsoft.BLL.Interfaces;
using Lipsoft.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lipsoft.API.Controllers;

[ApiController]
[Route("creditApplications")]
public class CreditApplicationController(ICreditApplicationService creditApplicationService) : ControllerBase
{
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreditApplication))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    public async Task<ActionResult<CreditApplication>> GetCreditApplication([FromRoute] long id, CancellationToken cancellationToken)
    {
        var result = await creditApplicationService.GetCreditApplicationByIdAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                NotFoundError => NotFound(result.Error.Message), 
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message) 
            };
        }
    
        return Ok(result.Value); 
    }
    
    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IAsyncEnumerable<CreditApplication>))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<CreditApplication>>> GetCreditApplications([FromBody] CreditApplicationFilterDto filterDto, CancellationToken cancellationToken = default)
    {
        var filter = filterDto.ToCreditApplicationFilter();
        
        var result = await creditApplicationService.GetCreditApplicationsAsync(filter, cancellationToken);
    
        if (result.IsFailed)
        {
            return result.Error switch
            {
                ValidationError => BadRequest(result.Error.Message), 
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message) 
            };
        }

        return Ok(result.Value); 
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<ActionResult<long>> AddCreditApplication([FromBody] AddCreditApplicationDto addCreditApplicationDto, CancellationToken cancellationToken)
    {
        var creditApplication = addCreditApplicationDto.ToCreditApplication();
    
        var result = await creditApplicationService.AddCreditApplicationAsync(creditApplication, cancellationToken);
    
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
    
    [HttpPut("{id:long}")] 
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreditApplication))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<ActionResult<CreditApplication>> UpdateCreditApplication([FromRoute] long id, [FromBody] UpdateCreditApplicationDto updateCreditApplicationDto, CancellationToken cancellationToken)
    {
        var creditApplication = updateCreditApplicationDto.ToCreditApplication(id);
    
        var result = await creditApplicationService.UpdateCreditApplicationAsync(creditApplication, cancellationToken);

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
    public async Task<IActionResult> DeleteCreditApplication([FromRoute] long id, CancellationToken cancellationToken)
    {
        var result = await creditApplicationService.DeleteCreditApplicationAsync(id, cancellationToken);

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