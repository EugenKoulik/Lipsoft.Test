using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

/// <summary>
/// Defines the contract for the repository responsible for managing call data.
/// </summary>
public interface ICallRepository
{
    /// <summary>
    /// Retrieves a paginated list of calls asynchronously.
    /// </summary>
    /// <param name="offset">The number of records to skip for pagination.</param>
    /// <param name="size">The number of records to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous stream of <see cref="Call"/> entities.</returns>
    IAsyncEnumerable<Call> GetCallsAsync(int offset, int size, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific call by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the call to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="Call"/> as the result, or null if not found.</returns>
    Task<Call?> GetCallByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new call record to the database asynchronously.
    /// </summary>
    /// <param name="call">The <see cref="Call"/> object to be added.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with the ID of the newly created call as the result.</returns>
    Task<long> AddCallAsync(Call call, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing call record in the database asynchronously.
    /// </summary>
    /// <param name="call">The <see cref="Call"/> object containing updated data.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateCallAsync(Call call, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a specific call record by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the call to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteCallAsync(long id, CancellationToken cancellationToken);
}