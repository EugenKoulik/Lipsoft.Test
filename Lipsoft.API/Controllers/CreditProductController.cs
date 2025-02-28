using Lipsoft.API.Dtos.Requests.CreditProduct;
using Lipsoft.BLL.Infrastructure.Errors;
using Lipsoft.BLL.Interfaces;
using Lipsoft.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lipsoft.API.Controllers;


[ApiController]
[Route("creditProducts")]
public class CreditProductController : ControllerBase
{
    private readonly ICreditProductService _creditProductService;

    public CreditProductController(ICreditProductService creditProductService)
    {
        _creditProductService = creditProductService;
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCreditProduct(int id, CancellationToken cancellationToken)
    {
        var result = await _creditProductService.GetCreditProductByIdAsync(id, cancellationToken);

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

    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCreditProducts([FromQuery] int offSet, [FromQuery] int size , CancellationToken cancellationToken = default)
    {
        var result = await _creditProductService.GetCreditProductsAsync(offSet, size, cancellationToken);

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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddCreditProduct([FromBody] AddCreditProductDto addCreditProductDto, CancellationToken cancellationToken)
    {
        var creditProduct = addCreditProductDto.ToCreditProduct();

        var result = await _creditProductService.AddCreditProductAsync(creditProduct, cancellationToken);

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
    public async Task<IActionResult> UpdateCreditProduct(long id, [FromBody] UpdateCreditProductDto updateCreditProductDto, CancellationToken cancellationToken)
    {
        var creditProduct = updateCreditProductDto.ToCreditProduct(id);

        var result = await _creditProductService.UpdateCreditProductAsync(creditProduct, cancellationToken);

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
    public async Task<IActionResult> DeleteCreditProduct(int id, CancellationToken cancellationToken)
    {
        var result = await _creditProductService.DeleteCreditProductAsync(id, cancellationToken);

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