using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Validators;

public static partial class ClientValidator
{
    public static List<string> Validate(ClientModel clientModel)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(clientModel.FullName))
            errors.Add("FullName is required");

        if (clientModel.FullName is { Length: > 100 })
            errors.Add("FullName cannot be longer than 100 characters");

        if (clientModel.Age is < 18 or > 100)
            errors.Add("Age must be between 18 and 100");

        if (clientModel.Workplace is { Length: > 100 })
            errors.Add("Workplace cannot be longer than 100 characters");

        if (string.IsNullOrEmpty(clientModel.Phone))
            errors.Add("Phone is required");

        if (clientModel.Phone != null && !MyRegex().IsMatch(clientModel.Phone))
            errors.Add("Phone must be in format XXX-XXX-XXXX");

        return errors;
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"^\d{3}-\d{3}-\d{4}$")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
}