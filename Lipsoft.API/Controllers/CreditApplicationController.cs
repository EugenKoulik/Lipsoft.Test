using Lipsoft.API.Dtos.Requests.CreditApplication;
using Lipsoft.BLL.Infrastructure.Errors;
using Lipsoft.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lipsoft.API.Controllers;

[ApiController]
[Route("creditApplications")]
public class CreditApplicationController : ControllerBase
{
    private readonly ICreditApplicationService _creditApplicationService;

    public CreditApplicationController(ICreditApplicationService creditApplicationService)
    {
        _creditApplicationService = creditApplicationService;
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    public async Task<IActionResult> GetCreditApplication(long id, CancellationToken cancellationToken)
    {
        var result = await _creditApplicationService.GetCreditApplicationByIdAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                NotFoundError => NotFound(result.Error.Message), 
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message) 
            };
        }
    
        return Ok(result.GetValue()); 
    }
    
    [HttpPost("filter")]
    public async Task<IActionResult> GetCreditApplications([FromBody] CreditApplicationFilterDto filterDto, CancellationToken cancellationToken = default)
    {
        var filter = filterDto.ToCreditApplicationFilter();
        
        var result = await _creditApplicationService.GetCreditApplicationsAsync(filter, cancellationToken);
    
        if (result.IsFailed)
        {
            return result.Error switch
            {
                ValidationError => BadRequest(result.Error.Message), 
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message) 
            };
        }

        return Ok(result.GetValue()); 
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> AddCreditApplication([FromBody] AddCreditApplicationDto addCreditApplicationDto, CancellationToken cancellationToken)
    {
        var creditApplication = addCreditApplicationDto.ToCreditApplication();
    
        var result = await _creditApplicationService.AddCreditApplicationAsync(creditApplication, cancellationToken);
    
        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                ValidationError => BadRequest(result.Error.Message), 
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message) 
            };
        }

        return Ok(result.GetValue());
    }
    
    [HttpPut("{id:long}")] 
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> UpdateCreditApplication(long id, [FromBody] UpdateCreditApplicationDto updateCreditApplicationDto, CancellationToken cancellationToken)
    {
        var creditApplication = updateCreditApplicationDto.ToCreditApplication(id);
    
        var result = await _creditApplicationService.UpdateCreditApplicationAsync(creditApplication, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                NotFoundError => NotFound(result.Error.Message), 
                ValidationError => BadRequest(result.Error.Message), 
                _ => StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message) 
            };
        }

        return Ok(result.GetValue()); 
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCreditApplication(int id, CancellationToken cancellationToken)
    {
        var result = await _creditApplicationService.DeleteCreditApplicationAsync(id, cancellationToken);

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