using Lipsoft.BLL.Infrastructure;

namespace Lipsoft.BLL.Validators;

public static class PaginationValidator
{
    public static List<string> Validate(int offset, int size)
    {
        var errors = new List<string>();
    
        if (offset < 0 || size < 1)  
        {
            errors.Add("Offset and page size must be greater than 0.");
        }
    
        if (offset > AppConstants.MaxPageOffset)
        {
            errors.Add($"Offset cannot be greater than {AppConstants.MaxPageOffset}.");
        }

        if (size > AppConstants.MaxPageSize)
        {
            errors.Add($"Page size cannot be greater than {AppConstants.MaxPageSize}.");
        }
    
        return errors;
    }
}