using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

public interface IClientRepository
{
    IAsyncEnumerable<Client> GetAllClientsAsync(CancellationToken cancellationToken = default);
    Task<Client?> GetClientByIdAsync(long id, CancellationToken cancellationToken);
    Task<long> AddClientAsync(Client client, CancellationToken cancellationToken);
    Task UpdateClientAsync(Client client, CancellationToken cancellationToken);
    Task DeleteClientAsync(long id, CancellationToken cancellationToken);
}