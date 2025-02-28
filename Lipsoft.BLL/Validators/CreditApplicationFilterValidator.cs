using Lipsoft.BLL.Infrastructure;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Validators;

public static class CreditApplicationFilterValidator
{
    public static List<string> Validate(CreditApplicationFilter filter)
    {
        var errors = new List<string>();

        if (filter.LoanPurpose != null && !Enum.IsDefined(typeof(LoanPurpose), filter.LoanPurpose))
        {
            errors.Add("LoanPurpose has an invalid value.");
        }

        if (filter.CreditProductId is <= 0)
        {
            errors.Add("CreditProductId must be greater than 0.");
        }

        if (filter.MinLoanAmount is < 0)
        {
            errors.Add("MinLoanAmount must be greater than or equal to 0.");
        }

        if (filter.MaxLoanAmount is < 0)
        {
            errors.Add("MaxLoanAmount must be greater than or equal to 0.");
        }

        if (filter is { MinLoanAmount: not null, MaxLoanAmount: not null } && filter.MinLoanAmount > filter.MaxLoanAmount)
        {
            errors.Add("MinLoanAmount must be less than or equal to MaxLoanAmount.");
        }

        if (filter.Size <= 0)
        {
            errors.Add("Size must be greater than 0.");
        }
        else if (filter.Size > AppConstants.MaxPageSize)
        {
            errors.Add($"Size must be less than or equal to {AppConstants.MaxPageSize}.");
        }

        if (filter.Offset < 0)
        {
            errors.Add("Offset must be greater than or equal to 0.");
        }
        else if (filter.Offset > AppConstants.MaxPageOffset)
        {
            errors.Add($"Offset must be less than or equal to {AppConstants.MaxPageOffset}.");
        }

        return errors;
    }
}