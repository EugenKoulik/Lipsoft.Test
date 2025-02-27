using Lipsoft.API.Dtos.Requests.Client;
using Lipsoft.BLL.Errors;
using Lipsoft.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lipsoft.API.Controllers;

[ApiController]
[Route("clients")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    public async Task<IActionResult> GetClient(long id, CancellationToken cancellationToken)
    {
        var result = await clientService.GetClientById(id, cancellationToken);

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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> GetAllClients(CancellationToken cancellationToken)
    {
        var result = await clientService.GetAllClientsAsync(cancellationToken);
    
        if (!result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Error?.Message);
        }

        return Ok(result.Value); 
    }
    
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> AddClient([FromBody] AddClientDto addClientDto, CancellationToken cancellationToken)
    {
        var client = addClientDto.ToClient();
    
        var result = await clientService.AddClient(client, cancellationToken);
    
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
    public async Task<IActionResult> UpdateClient(long id, [FromBody] UpdateClientDto updateClientDto, CancellationToken cancellationToken)
    {
        var client = updateClientDto.ToClient(id);
    
        var result = await clientService.UpdateClient(client, cancellationToken);

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
    public async Task<IActionResult> DeleteClient(long id, CancellationToken cancellationToken)
    {
        var result = await clientService.DeleteClient(id, cancellationToken);

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