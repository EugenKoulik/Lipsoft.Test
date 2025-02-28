using Lipsoft.BLL.Infrastructure;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

/// <summary>
/// Defines the service interface for handling operations related to credit products.
/// </summary>
public interface ICreditProductService
{
    /// <summary>
    /// Retrieves a specific credit product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the credit product.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the credit product, or a failure result if the credit product was not found.</returns>
    Task<Result<CreditProduct?>> GetCreditProductByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a collection of credit products with pagination support.
    /// </summary>
    /// <param name="offset">The starting index of the credit products to retrieve.</param>
    /// <param name="size">The number of credit products to retrieve.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing a collection of credit products, or a failure result if the operation fails.</returns>
    Task<Result<IAsyncEnumerable<CreditProduct>>> GetCreditProductsAsync(int offset, int size, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new credit product to the system.
    /// </summary>
    /// <param name="creditProduct">The credit product to be added.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the ID of the newly created credit product, or a failure result if the operation fails.</returns>
    Task<Result<long>> AddCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing credit product in the system.
    /// </summary>
    /// <param name="creditProduct">The credit product with updated information.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> containing the updated credit product, or a failure result if the operation fails.</returns>
    Task<Result<CreditProduct?>> UpdateCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a credit product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the credit product to be deleted.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> indicating whether the deletion was successful, or a failure result if the operation fails.</returns>
    Task<Result<bool>> DeleteCreditProductAsync(long id, CancellationToken cancellationToken);
}