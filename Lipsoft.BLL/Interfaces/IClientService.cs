using Lipsoft.BLL.Infrastructure;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

/// <summary>
/// Defines the service interface for handling operations related to clients.
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Retrieves a client by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the client.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the client, or a failure result if the client was not found.</returns>
    Task<Result<Client?>> GetClientById(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new client to the system.
    /// </summary>
    /// <param name="client">The client to be added.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the ID of the newly created client, or a failure result if the operation fails.</returns>
    Task<Result<long>> AddClient(Client client, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing client in the system.
    /// </summary>
    /// <param name="client">The client with updated information.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the updated client, or a failure result if the operation fails.</returns>
    Task<Result<Client?>> UpdateClient(Client client, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a client by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the client to be deleted.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> indicating whether the deletion was successful, or a failure result if the operation fails.</returns>
    Task<Result<bool>> DeleteClient(long id, CancellationToken cancellationToken);
}