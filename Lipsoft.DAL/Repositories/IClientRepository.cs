using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

/// <summary>
/// Defines the contract for the repository responsible for managing client data.
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Retrieves a specific client by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the client to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="Client"/> as the result, or null if not found.</returns>
    Task<Client?> GetClientByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new client record to the database asynchronously.
    /// </summary>
    /// <param name="client">The <see cref="Client"/> object to be added.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with the ID of the newly created client as the result.</returns>
    Task<long> AddClientAsync(Client client, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing client record in the database asynchronously.
    /// </summary>
    /// <param name="client">The <see cref="Client"/> object containing updated data.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateClientAsync(Client client, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a specific client record by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the client to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteClientAsync(long id, CancellationToken cancellationToken);
}