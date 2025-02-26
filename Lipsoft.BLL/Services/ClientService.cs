using Lipsoft.BLL.Interfaces;
using Lipsoft.BLL.Services.Base;
using Lipsoft.BLL.Validators;
using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;

namespace Lipsoft.BLL.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<BaseServiceResult<ClientModel?>> GetClientById(int id)
    {
        var client = await _clientRepository.GetClientByIdAsync(id);

        if (client == null)
        {
            return BaseServiceResult<ClientModel?>.Failure("Клиент не найден.");
        }

        return BaseServiceResult<ClientModel?>.Success(client);
    }
    
    public async Task<BaseServiceResult<IEnumerable<ClientModel>>> GetAllClientsAsync()
    {
        var clients = await _clientRepository.GetAllClientsAsync();

        return BaseServiceResult<IEnumerable<ClientModel>>.Success(clients);
    }
    
    public async Task<BaseServiceResult<ClientModel?>> AddClient(ClientModel clientModel)
    {
        var validationResult = ClientValidator.Validate(clientModel);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return BaseServiceResult<ClientModel?>.Failure(errorMessage);
        }
        
        await _clientRepository.AddClientAsync(clientModel);

        return BaseServiceResult<ClientModel?>.Success(clientModel);
    }
    
    public async Task<BaseServiceResult<ClientModel?>> UpdateClient(ClientModel clientModel)
    {
        var validationResult = ClientValidator.Validate(clientModel);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return BaseServiceResult<ClientModel?>.Failure(errorMessage);
        }

        var existingClient = await _clientRepository.GetClientByIdAsync(clientModel.Id);

        if (existingClient == null)
        {
            return BaseServiceResult<ClientModel?>.Failure("Клиент не найден.");
        }

        await _clientRepository.UpdateClientAsync(clientModel);
        
        return BaseServiceResult<ClientModel?>.Success(clientModel);
    }
    
    public async Task<BaseServiceResult<bool>> DeleteClient(int id)
    {
        var client = await _clientRepository.GetClientByIdAsync(id);

        if (client == null)
        {
            return BaseServiceResult<bool>.Failure("Клиент не найден.");
        }

        await _clientRepository.DeleteClientAsync(id);
        
        return BaseServiceResult<bool>.Success(true);
    }
}