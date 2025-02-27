using Lipsoft.BLL.Errors;
using Lipsoft.BLL.Interfaces;
using Lipsoft.BLL.Services.Base;
using Lipsoft.BLL.Validators;
using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;

namespace Lipsoft.BLL.Services;

public class CreditApplicationService : ICreditApplicationService
{
    private readonly ICreditApplicationRepository _creditApplicationRepository;
    private readonly ICallService _callService;

    public CreditApplicationService(ICreditApplicationRepository creditApplicationRepository, ICallService callService)
    {
        _creditApplicationRepository = creditApplicationRepository;
        _callService = callService;
    }

    public async Task<BaseServiceResult<(IEnumerable<CreditApplication> CreditApplications, int TotalCount)>> GetCreditApplicationsAsync(
        LoanPurpose? loanPurpose = null,
        long? creditProductId = null,
        decimal? minLoanAmount = null,
        decimal? maxLoanAmount = null,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return BaseServiceResult<(IEnumerable<CreditApplication>, int)>.Failure(
                new ValidationError("Номер страницы и размер страницы должны быть больше 0."));
        }

        var result = await _creditApplicationRepository.GetCreditApplicationsAsync(
            loanPurpose, creditProductId, minLoanAmount, maxLoanAmount, pageNumber, pageSize, cancellationToken);

        return BaseServiceResult<(IEnumerable<CreditApplication>, int)>.Success(result);
    }

    public async Task<BaseServiceResult<CreditApplication?>> GetCreditApplicationByIdAsync(long id, CancellationToken cancellationToken)
    {
        var creditApplication = await _creditApplicationRepository.GetCreditApplicationByIdAsync(id, cancellationToken);

        if (creditApplication == null)
        {
            return BaseServiceResult<CreditApplication?>.Failure(new NotFoundError("Кредитная заявка не найдена."));
        }

        return BaseServiceResult<CreditApplication?>.Success(creditApplication);
    }

    public async Task<BaseServiceResult<long>> AddCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken)
    {
        var validationResult = CreditApplicationValidator.Validate(creditApplication);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            return BaseServiceResult<long>.Failure(new ValidationError(errorMessage));
        }
        
        var newCreditApplicationId = await _creditApplicationRepository.AddCreditApplicationAsync(creditApplication, cancellationToken);

        var call = new Call
        {
            ScheduledDate = DateTime.UtcNow.AddMinutes(10),
            CallResult = null, 
            Status = CallStatus.Scheduled 
        };
        
        //TODO продумать логику отката транзакции

        var callResult = await _callService.AddCallAsync(call, cancellationToken); 

        return BaseServiceResult<long>.Success(newCreditApplicationId);
    }

    public async Task<BaseServiceResult<CreditApplication?>> UpdateCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken)
    {
        var validationResult = CreditApplicationValidator.Validate(creditApplication);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return BaseServiceResult<CreditApplication?>.Failure(new ValidationError(errorMessage));
        }

        var existingCreditApplication = await _creditApplicationRepository.GetCreditApplicationByIdAsync(creditApplication.Id, cancellationToken);

        if (existingCreditApplication == null)
        {
            return BaseServiceResult<CreditApplication?>.Failure(new NotFoundError("Кредитная заявка не найдена."));
        }

        await _creditApplicationRepository.UpdateCreditApplicationAsync(creditApplication, cancellationToken);
        
        return BaseServiceResult<CreditApplication?>.Success(creditApplication);
    }

    public async Task<BaseServiceResult<bool>> DeleteCreditApplicationAsync(long id, CancellationToken cancellationToken)
    {
        var creditApplication = await _creditApplicationRepository.GetCreditApplicationByIdAsync(id, cancellationToken);

        if (creditApplication == null)
        {
            return BaseServiceResult<bool>.Failure(new NotFoundError("Кредитная заявка не найдена."));
        }

        await _creditApplicationRepository.DeleteCreditApplicationAsync(id, cancellationToken);
        
        return BaseServiceResult<bool>.Success(true);
    }
}