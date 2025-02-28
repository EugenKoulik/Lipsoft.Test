using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Validators;

public static class CreditProductValidator
{
    public static List<string> Validate(CreditProduct creditProduct)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(creditProduct.ProductName))
            errors.Add("ProductName is required");

        if (creditProduct.InterestRate is <= 0 or > 100)
            errors.Add("InterestRate must be between 0 and 100");

        return errors;
    }
}