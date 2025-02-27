using Lipsoft.BLL.Errors;
using Lipsoft.BLL.Interfaces;
using Lipsoft.BLL.Services.Base;
using Lipsoft.BLL.Validators;
using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;

namespace Lipsoft.BLL.Services;

public class CallService : ICallService
{
    private readonly ICallRepository _callRepository;

    public CallService(ICallRepository callRepository)
    {
        _callRepository = callRepository;
    }

    public async Task<BaseServiceResult<Call?>> GetCallByIdAsync(long id, CancellationToken cancellationToken)
    {
        var call = await _callRepository.GetCallByIdAsync(id, cancellationToken);

        if (call == null)
        {
            return BaseServiceResult<Call?>.Failure(new NotFoundError("Вызов не найден."));
        }

        return BaseServiceResult<Call?>.Success(call);
    }

    public async Task<BaseServiceResult<(IEnumerable<Call> Calls, int TotalCount)>> GetAllCallsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return BaseServiceResult<(IEnumerable<Call>, int)>.Failure(new ValidationError("Номер страницы и размер страницы должны быть больше 0."));
        }

        var result = await _callRepository.GetAllCallsAsync(pageNumber, pageSize, cancellationToken);
        
        return BaseServiceResult<(IEnumerable<Call>, int)>.Success(result);
    }

    public async Task<BaseServiceResult<long>> AddCallAsync(Call call, CancellationToken cancellationToken)
    {
        var validationResult = CallValidator.Validate(call);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return BaseServiceResult<long>.Failure(new ValidationError(errorMessage));
        }

        var newId = await _callRepository.AddCallAsync(call, cancellationToken);
        
        return BaseServiceResult<long>.Success(newId);
    }

    public async Task<BaseServiceResult<Call?>> UpdateCallAsync(Call call, CancellationToken cancellationToken)
    {
        var validationResult = CallValidator.Validate(call);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return BaseServiceResult<Call?>.Failure(new ValidationError(errorMessage));
        }

        var existingCall = await _callRepository.GetCallByIdAsync(call.Id, cancellationToken);

        if (existingCall == null)
        {
            return BaseServiceResult<Call?>.Failure(new NotFoundError("Вызов не найден."));
        }

        await _callRepository.UpdateCallAsync(call, cancellationToken);
        
        return BaseServiceResult<Call?>.Success(call);
    }

    public async Task<BaseServiceResult<bool>> DeleteCallAsync(long id, CancellationToken cancellationToken)
    {
        var call = await _callRepository.GetCallByIdAsync(id, cancellationToken);

        if (call == null)
        {
            return BaseServiceResult<bool>.Failure(new NotFoundError("Вызов не найден."));
        }

        await _callRepository.DeleteCallAsync(id, cancellationToken);
        
        return BaseServiceResult<bool>.Success(true);
    }
}