using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

public interface ICreditApplicationRepository
{
    Task<(IEnumerable<CreditApplication> CreditApplications, int TotalCount)> GetCreditApplicationsAsync(
        LoanPurpose? loanPurpose = null,
        long? creditProductId = null,
        decimal? minLoanAmount = null,
        decimal? maxLoanAmount = null,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
    Task<CreditApplication?> GetCreditApplicationByIdAsync(long id, CancellationToken cancellationToken);
    Task<long> AddCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);
    Task UpdateCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);
    Task DeleteCreditApplicationAsync(long id, CancellationToken cancellationToken);
    
}