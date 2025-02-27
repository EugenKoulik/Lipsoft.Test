using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

public interface ICallRepository
{
    Task<(IEnumerable<Call> Calls, int TotalCount)> GetAllCallsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Call?> GetCallByIdAsync(long id, CancellationToken cancellationToken);
    Task<long> AddCallAsync(Call call, CancellationToken cancellationToken);
    Task UpdateCallAsync(Call call, CancellationToken cancellationToken);
    Task DeleteCallAsync(long id, CancellationToken cancellationToken);
    
}