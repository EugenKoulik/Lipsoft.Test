using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

/// <summary>
/// Defines the contract for the repository responsible for managing credit application data.
/// </summary>
public interface ICreditApplicationRepository
{
    /// <summary>
    /// Retrieves a list of credit applications based on the provided filter asynchronously.
    /// </summary>
    /// <param name="filter">The filter criteria used to retrieve the credit applications.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with an <see cref="IAsyncEnumerable{CreditApplication}"/> as the result.</returns>
    IAsyncEnumerable<CreditApplication> GetCreditApplicationsAsync(CreditApplicationFilter filter, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific credit application by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the credit application to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="CreditApplication"/> as the result, or null if not found.</returns>
    Task<CreditApplication?> GetCreditApplicationByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new credit application record to the database asynchronously. 
    /// </summary>
    /// <param name="creditApplication">The <see cref="CreditApplication"/> object to be added.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with the ID of the newly created credit application as the result.</returns>
    Task<long> AddCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing credit application record in the database asynchronously.
    /// </summary>
    /// <param name="creditApplication">The <see cref="CreditApplication"/> object containing updated data.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a specific credit application record by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the credit application to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteCreditApplicationAsync(long id, CancellationToken cancellationToken);
}