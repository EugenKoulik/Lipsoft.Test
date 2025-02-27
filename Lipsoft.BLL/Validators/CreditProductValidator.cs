using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Validators;

public static class CreditProductValidator
{
    public static List<string> Validate(CreditProduct creditProduct)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(creditProduct.ProductName))
            errors.Add("ProductName is required");

        if (creditProduct.InterestRate <= 0 || creditProduct.InterestRate > 100)
            errors.Add("InterestRate must be between 0 and 100");

        if (creditProduct.Id <= 0)
            errors.Add("Id must be greater than 0");

        return errors;
    }
}