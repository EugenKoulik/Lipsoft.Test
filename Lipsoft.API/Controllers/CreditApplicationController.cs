using Lipsoft.API.Dtos.Requests.CreditApplication;
using Lipsoft.BLL.Errors;
using Lipsoft.BLL.Interfaces;
using Lipsoft.Data.Models;
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
                _ => BadRequest(result.Error?.Message) 
            };
        }
    
        return Ok(result.Value); 
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> GetCreditApplications(
        [FromQuery] LoanPurpose? loanPurpose = null,
        [FromQuery] long? creditProductId = null,
        [FromQuery] decimal? minLoanAmount = null,
        [FromQuery] decimal? maxLoanAmount = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _creditApplicationService.GetCreditApplicationsAsync(
            loanPurpose,
            creditProductId,
            minLoanAmount,
            maxLoanAmount,
            pageNumber,
            pageSize,
            cancellationToken);
    
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
            CreditApplications = result.Value.CreditApplications,
            TotalCount = result.Value.TotalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        }); 
    }
    
    [HttpPost("add")]
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

        return Ok(result.Value);
    }
    
    [HttpPut("update/{id:long}")] 
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> UpdateCreditApplication(int id, [FromBody] UpdateCreditApplicationDto updateCreditApplicationDto, CancellationToken cancellationToken)
    {
        var creditApplication = updateCreditApplicationDto.ToCreditApplication();
    
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

        return Ok(result.Value); 
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