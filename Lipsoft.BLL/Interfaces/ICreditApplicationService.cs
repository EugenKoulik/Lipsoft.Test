using Lipsoft.BLL.Infrastructure;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

/// <summary>
/// Defines the service interface for handling operations related to credit applications.
/// </summary>
public interface ICreditApplicationService
{
    /// <summary>
    /// Retrieves a collection of credit applications based on the provided filter criteria.
    /// </summary>
    /// <param name="filter">The filter criteria to apply when retrieving the credit applications.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing a collection of credit applications, or a failure result if the operation fails.</returns>
    Task<Result<IAsyncEnumerable<CreditApplication>>> GetCreditApplicationsAsync(CreditApplicationFilter filter, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific credit application by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the credit application.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the credit application, or a failure result if the credit application was not found.</returns>
    Task<Result<CreditApplication?>> GetCreditApplicationByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new credit application to the system. 
    /// A call is also created automatically when a credit application is added, scheduled for a future date.
    /// </summary>
    /// <param name="creditApplication">The credit application to be added.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the ID of the newly created credit application, or a failure result if the operation fails.</returns>
    Task<Result<long>> AddCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing credit application in the system.
    /// </summary>
    /// <param name="creditApplication">The credit application with updated information.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the updated credit application, or a failure result if the operation fails.</returns>
    Task<Result<CreditApplication?>> UpdateCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a credit application by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the credit application to be deleted.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> indicating whether the deletion was successful, or a failure result if the operation fails.</returns>
    Task<Result<bool>> DeleteCreditApplicationAsync(long id, CancellationToken cancellationToken);
}