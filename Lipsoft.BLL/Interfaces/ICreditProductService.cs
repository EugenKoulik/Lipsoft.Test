using Lipsoft.BLL.Services.Base;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

public interface ICreditProductService
{
    Task<BaseServiceResult<CreditProduct?>> GetCreditProductByIdAsync(long id, CancellationToken cancellationToken);
    Task<BaseServiceResult<(IEnumerable<CreditProduct> CreditProducts, int TotalCount)>> GetAllCreditProductsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<BaseServiceResult<long>> AddCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);
    Task<BaseServiceResult<CreditProduct?>> UpdateCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);
    Task<BaseServiceResult<bool>> DeleteCreditProductAsync(long id, CancellationToken cancellationToken);
}