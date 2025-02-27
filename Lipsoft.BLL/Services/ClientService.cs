using Lipsoft.BLL.Errors;
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

    public async Task<BaseServiceResult<Client?>> GetClientById(long id, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientByIdAsync(id, cancellationToken);

        if (client == null)
        {
            return BaseServiceResult<Client?>.Failure(new NotFoundError("Клиент не найден."));
        }

        return BaseServiceResult<Client?>.Success(client);
    }
    
    public async Task<BaseServiceResult<IEnumerable<Client>>> GetAllClientsAsync(CancellationToken cancellationToken)
    {
        var clients = new List<Client>();

        await foreach (var client in _clientRepository.GetAllClientsAsync(cancellationToken))
        {
            clients.Add(client);
        }

        return BaseServiceResult<IEnumerable<Client>>.Success(clients);
    }
    
    public async Task<BaseServiceResult<long>> AddClient(Client client, CancellationToken cancellationToken)
    {
        var validationResult = ClientValidator.Validate(client);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return BaseServiceResult<long>.Failure(new ValidationError(errorMessage));
        }
        
        var newId = await _clientRepository.AddClientAsync(client, cancellationToken);

        return BaseServiceResult<long>.Success(newId);
    }
    
    public async Task<BaseServiceResult<Client?>> UpdateClient(Client client, CancellationToken cancellationToken)
    {
        var validationResult = ClientValidator.Validate(client);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return BaseServiceResult<Client?>.Failure(new ValidationError(errorMessage));
        }

        var existingClient = await _clientRepository.GetClientByIdAsync(client.Id, cancellationToken);

        if (existingClient == null)
        {
            return BaseServiceResult<Client?>.Failure(new NotFoundError("Клиент не найден."));
        }

        await _clientRepository.UpdateClientAsync(client, cancellationToken);
        
        return BaseServiceResult<Client?>.Success(client);
    }
    
    public async Task<BaseServiceResult<bool>> DeleteClient(long id, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientByIdAsync(id, cancellationToken);

        if (client == null)
        {
            return BaseServiceResult<bool>.Failure(new NotFoundError("Клиент не найден."));
        }

        await _clientRepository.DeleteClientAsync(id, cancellationToken);
        
        return BaseServiceResult<bool>.Success(true);
    }
}