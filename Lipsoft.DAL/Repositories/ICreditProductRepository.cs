using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

public interface ICreditProductRepository
{
    Task<(IEnumerable<CreditProduct> CreditProducts, int TotalCount)> GetCreditProductsAsync(
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
    Task<CreditProduct?> GetCreditProductByIdAsync(long id, CancellationToken cancellationToken);
    Task<long> AddCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);
    Task UpdateCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);
    Task DeleteCreditProductAsync(long id, CancellationToken cancellationToken);
}
