using Lipsoft.BLL.Infrastructure;
using Lipsoft.BLL.Infrastructure.Errors;
using Lipsoft.BLL.Interfaces;
using Lipsoft.BLL.Validators;
using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;

namespace Lipsoft.BLL.Services;

public class CreditProductService : ICreditProductService
{
    private readonly ICreditProductRepository _creditProductRepository;

    public CreditProductService(ICreditProductRepository creditProductRepository)
    {
        _creditProductRepository = creditProductRepository;
    }

    public async Task<Result<CreditProduct?>> GetCreditProductByIdAsync(long id, CancellationToken cancellationToken)
    {
        var creditProduct = await _creditProductRepository.GetCreditProductByIdAsync(id, cancellationToken);

        if (creditProduct == null)
        {
            return Result<CreditProduct?>.Failure(new NotFoundError("Кредитный продукт не найден."));
        }

        return Result<CreditProduct?>.Success(creditProduct);
    }

    public Task<Result<IAsyncEnumerable<CreditProduct>>> GetCreditProductsAsync(int offset, int size, CancellationToken cancellationToken)
    {
        var validationResult = PaginationValidator.Validate(offset, size);
        
        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Task.FromResult(Result<IAsyncEnumerable<CreditProduct>>.Failure(new ValidationError(errorMessage)));
        }

        var result = _creditProductRepository.GetCreditProductsAsync(offset, size, cancellationToken);
    
        return Task.FromResult(Result<IAsyncEnumerable<CreditProduct>>.Success(result));
    }

    public async Task<Result<long>> AddCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken)
    {
        var validationResult = CreditProductValidator.Validate(creditProduct);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Result<long>.Failure(new ValidationError(errorMessage));
        }

        var newId = await _creditProductRepository.AddCreditProductAsync(creditProduct, cancellationToken);
        
        return Result<long>.Success(newId);
    }

    public async Task<Result<CreditProduct?>> UpdateCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken)
    {
        var validationResult = CreditProductValidator.Validate(creditProduct);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return Result<CreditProduct?>.Failure(new ValidationError(errorMessage));
        }

        var existingCreditProduct = await _creditProductRepository.GetCreditProductByIdAsync(creditProduct.Id, cancellationToken);

        if (existingCreditProduct == null)
        {
            return Result<CreditProduct?>.Failure(new NotFoundError("Кредитный продукт не найден."));
        }

        await _creditProductRepository.UpdateCreditProductAsync(creditProduct, cancellationToken);
        
        return Result<CreditProduct?>.Success(creditProduct);
    }

    public async Task<Result<bool>> DeleteCreditProductAsync(long id, CancellationToken cancellationToken)
    {
        var creditProduct = await _creditProductRepository.GetCreditProductByIdAsync(id, cancellationToken);

        if (creditProduct == null)
        {
            return Result<bool>.Failure(new NotFoundError("Кредитный продукт не найден."));
        }

        await _creditProductRepository.DeleteCreditProductAsync(id, cancellationToken);
        
        return Result<bool>.Success(true);
    }
}