using Lipsoft.BLL.Infrastructure;
using Lipsoft.BLL.Infrastructure.Errors;
using Lipsoft.BLL.Interfaces;
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

    public async Task<Result<Client?>> GetClientById(long id, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientByIdAsync(id, cancellationToken);

        if (client == null)
        {
            return Result<Client?>.Failure(new NotFoundError("Клиент не найден."));
        }

        return Result<Client?>.Success(client);
    }
    
    public async Task<Result<long>> AddClient(Client client, CancellationToken cancellationToken)
    {
        var validationResult = ClientValidator.Validate(client);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Result<long>.Failure(new ValidationError(errorMessage));
        }
        
        var newId = await _clientRepository.AddClientAsync(client, cancellationToken);

        return Result<long>.Success(newId);
    }
    
    public async Task<Result<Client?>> UpdateClient(Client client, CancellationToken cancellationToken)
    {
        var validationResult = ClientValidator.Validate(client);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Result<Client?>.Failure(new ValidationError(errorMessage));
        }

        var existingClient = await _clientRepository.GetClientByIdAsync(client.Id, cancellationToken);

        if (existingClient == null)
        {
            return Result<Client?>.Failure(new NotFoundError("Клиент не найден."));
        }

        await _clientRepository.UpdateClientAsync(client, cancellationToken);
        
        return Result<Client?>.Success(client);
    }
    
    public async Task<Result<bool>> DeleteClient(long id, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientByIdAsync(id, cancellationToken);

        if (client == null)
        {
            return Result<bool>.Failure(new NotFoundError("Клиент не найден."));
        }

        await _clientRepository.DeleteClientAsync(id, cancellationToken);
        
        return Result<bool>.Success(true);
    }
}