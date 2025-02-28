using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

/// <summary>
/// Defines the contract for the repository responsible for managing credit product data.
/// </summary>
public interface ICreditProductRepository
{
    /// <summary>
    /// Retrieves a list of credit products asynchronously, with support for pagination.
    /// </summary>
    /// <param name="offset">The number of items to skip (offset) for pagination.</param>
    /// <param name="size">The number of items to retrieve (size) for pagination.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with an <see cref="IAsyncEnumerable{CreditProduct}"/> as the result.</returns>
    IAsyncEnumerable<CreditProduct> GetCreditProductsAsync(int offset, int size, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific credit product by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the credit product to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="CreditProduct"/> as the result, or null if not found.</returns>
    Task<CreditProduct?> GetCreditProductByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new credit product record to the database asynchronously.
    /// </summary>
    /// <param name="creditProduct">The <see cref="CreditProduct"/> object to be added.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with the ID of the newly created credit product as the result.</returns>
    Task<long> AddCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing credit product record in the database asynchronously.
    /// </summary>
    /// <param name="creditProduct">The <see cref="CreditProduct"/> object containing updated data.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a specific credit product record by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the credit product to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteCreditProductAsync(long id, CancellationToken cancellationToken);
}
