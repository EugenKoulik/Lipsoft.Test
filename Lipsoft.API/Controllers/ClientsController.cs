using Lipsoft.API.Dtos.Client;
using Lipsoft.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lipsoft.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
[Route("clients")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var result = await clientService.GetClientById(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }
        
        return Ok(result.Value);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllClients()
    {
        var result = await clientService.GetAllClientsAsync();
        
        return Ok(result.Value); 
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddClient([FromBody] ClientDto client)
    {
        var clientModel = ClientMapper.ToClientModel(client);
        
        var result = await clientService.AddClient(clientModel);
        
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateClient([FromBody] ClientDto client)
    {
        var clientModel = ClientMapper.ToClientModel(client);
        
        var result = await clientService.UpdateClient(clientModel);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value); 
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var result = await clientService.DeleteClient(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage); 
        }

        return NoContent(); 
    }
}