using System.Transactions;
using Lipsoft.BLL.Infrastructure;
using Lipsoft.BLL.Infrastructure.Errors;
using Lipsoft.BLL.Interfaces;
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

    public Task<Result<IAsyncEnumerable<CreditApplication>>> GetCreditApplicationsAsync(CreditApplicationFilter filter, CancellationToken cancellationToken)
    {
        var validationResult = CreditApplicationFilterValidator.Validate(filter);
        
        if (validationResult.Count != 0)  
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Task.FromResult(Result<IAsyncEnumerable<CreditApplication>>.Failure(new ValidationError(errorMessage)));
        }

        var result = _creditApplicationRepository.GetCreditApplicationsAsync(filter, cancellationToken);

        return Task.FromResult(Result<IAsyncEnumerable<CreditApplication>>.Success(result));
    }

    public async Task<Result<CreditApplication?>> GetCreditApplicationByIdAsync(long id, CancellationToken cancellationToken)
    {
        var creditApplication = await _creditApplicationRepository.GetCreditApplicationByIdAsync(id, cancellationToken);

        if (creditApplication == null)
        {
            return Result<CreditApplication?>.Failure(new NotFoundError("Кредитная заявка не найдена."));
        }

        return Result<CreditApplication?>.Success(creditApplication);
    }

    public async Task<Result<long>> AddCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken)
    {
        var validationResult = CreditApplicationValidator.Validate(creditApplication);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Result<long>.Failure(new ValidationError(errorMessage));
        }

        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        try
        {
            var newCreditApplicationId = await _creditApplicationRepository.AddCreditApplicationAsync(creditApplication, cancellationToken);

            var call = new Call
            {
                ScheduledDate = DateTime.UtcNow.AddMinutes(10),
                CallResult = null,
                Status = CallStatus.Scheduled
            };

            var callResult = await _callService.AddCallAsync(call, cancellationToken);

            if (!callResult.IsSuccess)
            {
                throw new Exception("Failed to create the call.");
            }

            transactionScope.Complete();

            return Result<long>.Success(newCreditApplicationId);
        }
        catch (Exception ex)
        {
            return Result<long>.Failure(new InternalError("Произошла ошибка при создании звонка. Изменения отменены."));
        }
    }

    public async Task<Result<CreditApplication?>> UpdateCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken)
    {
        var validationResult = CreditApplicationValidator.Validate(creditApplication);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Result<CreditApplication?>.Failure(new ValidationError(errorMessage));
        }

        var existingCreditApplication = await _creditApplicationRepository.GetCreditApplicationByIdAsync(creditApplication.Id, cancellationToken);

        if (existingCreditApplication == null)
        {
            return Result<CreditApplication?>.Failure(new NotFoundError("Кредитная заявка не найдена."));
        }

        await _creditApplicationRepository.UpdateCreditApplicationAsync(creditApplication, cancellationToken);
        
        return Result<CreditApplication?>.Success(creditApplication);
    }

    public async Task<Result<bool>> DeleteCreditApplicationAsync(long id, CancellationToken cancellationToken)
    {
        var creditApplication = await _creditApplicationRepository.GetCreditApplicationByIdAsync(id, cancellationToken);

        if (creditApplication == null)
        {
            return Result<bool>.Failure(new NotFoundError("Кредитная заявка не найдена."));
        }

        await _creditApplicationRepository.DeleteCreditApplicationAsync(id, cancellationToken);
        
        return Result<bool>.Success(true);
    }
}