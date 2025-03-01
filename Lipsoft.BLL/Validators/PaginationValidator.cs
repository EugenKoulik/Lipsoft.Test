namespace Lipsoft.BLL.Validators;

public static class PaginationValidator
{
    public static List<string> Validate(int offset, int size)
    {
        var maxPageSize = 1000; 
        var maxPageOffset = 100000; 
        
        var errors = new List<string>();
    
        if (offset < 0 || size < 1)  
        {
            errors.Add("Offset and page size must be greater than 0.");
        }
        
        if (offset > maxPageOffset)
        {
            errors.Add($"Offset cannot be greater than {maxPageOffset}.");
        }

        if (size > maxPageSize)
        {
            errors.Add($"Page size cannot be greater than {maxPageSize}.");
        }
    
        return errors;
    }
}