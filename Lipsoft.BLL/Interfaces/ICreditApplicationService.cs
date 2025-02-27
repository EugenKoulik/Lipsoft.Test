using Lipsoft.BLL.Services.Base;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

public interface ICreditApplicationService
{
    Task<BaseServiceResult<(IEnumerable<CreditApplication> CreditApplications, int TotalCount)>> GetCreditApplicationsAsync(
        LoanPurpose? loanPurpose = null,
        long? creditProductId = null,
        decimal? minLoanAmount = null,
        decimal? maxLoanAmount = null,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
    Task<BaseServiceResult<CreditApplication?>> GetCreditApplicationByIdAsync(long id, CancellationToken cancellationToken);
    Task<BaseServiceResult<long>> AddCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);
    Task<BaseServiceResult<CreditApplication?>> UpdateCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);
    Task<BaseServiceResult<bool>> DeleteCreditApplicationAsync(long id, CancellationToken cancellationToken);
}