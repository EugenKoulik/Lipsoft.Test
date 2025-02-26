using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Validators;

public static class CreditApplicationValidator
{
    public static List<string> Validate(CreditApplicationModel creditApplicationModel)
    {
        var errors = new List<string>();

        if (creditApplicationModel.LoanPurpose == null)
            errors.Add("LoanPurpose is required");
        
        if (creditApplicationModel.LoanAmount is < 0 or > 1_000_000)
            errors.Add("LoanAmount must be between 0 and 1,000,000");

        if (creditApplicationModel.ClientIncome is < 0 or > 1_000_000)
            errors.Add("ClientIncome must be between 0 and 1,000,000");

        if (creditApplicationModel.CreditProductId <= 0)
            errors.Add("CreditProductId is required");

        return errors;
    }
}