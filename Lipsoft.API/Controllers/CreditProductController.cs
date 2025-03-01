using Lipsoft.API.Dtos.Requests.CreditProduct;
using Lipsoft.BLL.Infrastructure.Errors;
using Lipsoft.BLL.Interfaces;
using Lipsoft.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lipsoft.API.Controllers;


[ApiController]
[Route("creditProducts")]
public class CreditProductController(ICreditProductService creditProductService) : ControllerBase
{
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreditProduct))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CreditProduct>> GetCreditProduct([FromRoute] long id, CancellationToken cancellationToken)
    {
        var result = await creditProductService.GetCreditProductByIdAsync(id, cancellationToken);

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

    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IAsyncEnumerable<CreditProduct>))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<CreditProduct>>> GetAllCreditProducts([FromQuery] int offSet, [FromQuery] int size , CancellationToken cancellationToken)
    {
        var result = await creditProductService.GetCreditProductsAsync(offSet, size, cancellationToken);

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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<long>> AddCreditProduct([FromBody] AddCreditProductDto addCreditProductDto, CancellationToken cancellationToken)
    {
        var creditProduct = addCreditProductDto.ToCreditProduct();

        var result = await creditProductService.AddCreditProductAsync(creditProduct, cancellationToken);

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreditProduct))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreditProduct>> UpdateCreditProduct([FromRoute] long id, [FromBody] UpdateCreditProductDto updateCreditProductDto, CancellationToken cancellationToken)
    {
        var creditProduct = updateCreditProductDto.ToCreditProduct(id);

        var result = await creditProductService.UpdateCreditProductAsync(creditProduct, cancellationToken);

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
    public async Task<IActionResult> DeleteCreditProduct([FromRoute] long id, CancellationToken cancellationToken)
    {
        var result = await creditProductService.DeleteCreditProductAsync(id, cancellationToken);

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