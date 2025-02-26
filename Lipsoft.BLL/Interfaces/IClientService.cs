using Lipsoft.BLL.Services.Base;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

public interface IClientService
{
    Task<BaseServiceResult<ClientModel?>> GetClientById(int id);
    Task<BaseServiceResult<IEnumerable<ClientModel>>> GetAllClientsAsync();
    Task<BaseServiceResult<ClientModel?>> AddClient(ClientModel clientModel);
    Task<BaseServiceResult<ClientModel?>> UpdateClient(ClientModel clientModel);
    Task<BaseServiceResult<bool>> DeleteClient(int id);
}