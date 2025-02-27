using Lipsoft.BLL.Errors;
using Lipsoft.BLL.Interfaces;
using Lipsoft.BLL.Services.Base;
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

    public async Task<BaseServiceResult<CreditProduct?>> GetCreditProductByIdAsync(long id, CancellationToken cancellationToken)
    {
        var creditProduct = await _creditProductRepository.GetCreditProductByIdAsync(id, cancellationToken);

        if (creditProduct == null)
        {
            return BaseServiceResult<CreditProduct?>.Failure(new NotFoundError("Кредитный продукт не найден."));
        }

        return BaseServiceResult<CreditProduct?>.Success(creditProduct);
    }

    public async Task<BaseServiceResult<(IEnumerable<CreditProduct> CreditProducts, int TotalCount)>> GetAllCreditProductsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return BaseServiceResult<(IEnumerable<CreditProduct>, int)>.Failure(new ValidationError("Номер страницы и размер страницы должны быть больше 0."));
        }

        var result = await _creditProductRepository.GetCreditProductsAsync(pageNumber, pageSize, cancellationToken);
        
        return BaseServiceResult<(IEnumerable<CreditProduct>, int)>.Success(result);
    }

    public async Task<BaseServiceResult<long>> AddCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken)
    {
        var validationResult = CreditProductValidator.Validate(creditProduct);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return BaseServiceResult<long>.Failure(new ValidationError(errorMessage));
        }

        var newId = await _creditProductRepository.AddCreditProductAsync(creditProduct, cancellationToken);
        
        return BaseServiceResult<long>.Success(newId);
    }

    public async Task<BaseServiceResult<CreditProduct?>> UpdateCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken)
    {
        var validationResult = CreditProductValidator.Validate(creditProduct);

        if (validationResult.Count != 0)
        {
            var errorMessage = string.Join("\n", validationResult);
            
            return BaseServiceResult<CreditProduct?>.Failure(new ValidationError(errorMessage));
        }

        var existingCreditProduct = await _creditProductRepository.GetCreditProductByIdAsync(creditProduct.Id, cancellationToken);

        if (existingCreditProduct == null)
        {
            return BaseServiceResult<CreditProduct?>.Failure(new NotFoundError("Кредитный продукт не найден."));
        }

        await _creditProductRepository.UpdateCreditProductAsync(creditProduct, cancellationToken);
        
        return BaseServiceResult<CreditProduct?>.Success(creditProduct);
    }

    public async Task<BaseServiceResult<bool>> DeleteCreditProductAsync(long id, CancellationToken cancellationToken)
    {
        var creditProduct = await _creditProductRepository.GetCreditProductByIdAsync(id, cancellationToken);

        if (creditProduct == null)
        {
            return BaseServiceResult<bool>.Failure(new NotFoundError("Кредитный продукт не найден."));
        }

        await _creditProductRepository.DeleteCreditProductAsync(id, cancellationToken);
        
        return BaseServiceResult<bool>.Success(true);
    }
}