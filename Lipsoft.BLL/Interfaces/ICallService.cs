using Lipsoft.BLL.Services.Base;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

public interface ICallService
{
    Task<BaseServiceResult<Call?>> GetCallByIdAsync(long id, CancellationToken cancellationToken);
    Task<BaseServiceResult<(IEnumerable<Call> Calls, int TotalCount)>> GetAllCallsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<BaseServiceResult<long>> AddCallAsync(Call call, CancellationToken cancellationToken);
    Task<BaseServiceResult<Call?>> UpdateCallAsync(Call call, CancellationToken cancellationToken);
    Task<BaseServiceResult<bool>> DeleteCallAsync(long id, CancellationToken cancellationToken);
}