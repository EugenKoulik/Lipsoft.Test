using Lipsoft.BLL.Services.Base;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

public interface IClientService
{
    Task<BaseServiceResult<Client?>> GetClientById(long id, CancellationToken cancellationToken);
    Task<BaseServiceResult<IEnumerable<Client>>> GetAllClientsAsync(CancellationToken cancellationToken);
    Task<BaseServiceResult<long>> AddClient(Client client, CancellationToken cancellationToken);
    Task<BaseServiceResult<Client?>> UpdateClient(Client client, CancellationToken cancellationToken);
    Task<BaseServiceResult<bool>> DeleteClient(long id, CancellationToken cancellationToken);
}