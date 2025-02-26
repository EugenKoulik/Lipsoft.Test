using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<ClientModel>> GetAllClientsAsync();
    Task<ClientModel?> GetClientByIdAsync(int id);
    Task AddClientAsync(ClientModel client);
    Task UpdateClientAsync(ClientModel client);
    Task DeleteClientAsync(int id);
}