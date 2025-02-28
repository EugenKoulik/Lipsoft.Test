using Lipsoft.BLL.Infrastructure;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

/// <summary>
/// Defines the service interface for handling operations related to calls.
/// </summary>
public interface ICallService
{
    /// <summary>
    /// Retrieves a call by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the call.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the call, or a failure result if the call was not found.</returns>
    Task<Result<Call?>> GetCallByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of calls with pagination.
    /// </summary>
    /// <param name="offset">The number of records to skip.</param>
    /// <param name="size">The number of records to retrieve.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing an <see cref="IAsyncEnumerable{Call}"/> of calls, or a failure result if the operation fails.</returns>
    Task<Result<IAsyncEnumerable<Call>>> GetCallsAsync(int offset, int size, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new call to the system.
    /// </summary>
    /// <param name="call">The call to be added.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the ID of the newly created call, or a failure result if the operation fails.</returns>
    Task<Result<long>> AddCallAsync(Call call, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing call in the system.
    /// </summary>
    /// <param name="call">The call with updated information.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the updated call, or a failure result if the operation fails.</returns>
    Task<Result<Call?>> UpdateCallAsync(Call call, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a call by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the call to be deleted.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> indicating whether the deletion was successful, or a failure result if the operation fails.</returns>
    Task<Result<bool>> DeleteCallAsync(long id, CancellationToken cancellationToken);
}