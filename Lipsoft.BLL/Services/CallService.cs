using Lipsoft.BLL.Infrastructure;
using Lipsoft.BLL.Infrastructure.Errors;
using Lipsoft.BLL.Interfaces;
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

    public async Task<Result<Call?>> GetCallByIdAsync(long id, CancellationToken cancellationToken)
    {
        var call = await _callRepository.GetCallByIdAsync(id, cancellationToken);

        if (call == null)
        {
            return Result<Call?>.Failure(new NotFoundError("Вызов не найден."));
        }

        return Result<Call?>.Success(call);
    }

    public Task<Result<IAsyncEnumerable<Call>>> GetCallsAsync(int offset, int size, CancellationToken cancellationToken)
    {
        var validationResult = PaginationValidator.Validate(offset, size);
        
        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Task.FromResult(Result<IAsyncEnumerable<Call>>.Failure(new ValidationError(errorMessage)));
        }

        var result = _callRepository.GetCallsAsync(offset, size, cancellationToken);
    
        return Task.FromResult(Result<IAsyncEnumerable<Call>>.Success(result));
    }

    public async Task<Result<long>> AddCallAsync(Call call, CancellationToken cancellationToken)
    {
        var validationResult = CallValidator.Validate(call);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Result<long>.Failure(new ValidationError(errorMessage));
        }

        var newId = await _callRepository.AddCallAsync(call, cancellationToken);
        
        return Result<long>.Success(newId);
    }

    public async Task<Result<Call?>> UpdateCallAsync(Call call, CancellationToken cancellationToken)
    {
        var validationResult = CallValidator.Validate(call);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Result<Call?>.Failure(new ValidationError(errorMessage));
        }

        var existingCall = await _callRepository.GetCallByIdAsync(call.Id, cancellationToken);

        if (existingCall == null)
        {
            return Result<Call?>.Failure(new NotFoundError("Вызов не найден."));
        }

        await _callRepository.UpdateCallAsync(call, cancellationToken);
        
        return Result<Call?>.Success(call);
    }

    public async Task<Result<bool>> DeleteCallAsync(long id, CancellationToken cancellationToken)
    {
        var call = await _callRepository.GetCallByIdAsync(id, cancellationToken);

        if (call == null)
        {
            return Result<bool>.Failure(new NotFoundError("Вызов не найден."));
        }

        await _callRepository.DeleteCallAsync(id, cancellationToken);
        
        return Result<bool>.Success(true);
    }
}