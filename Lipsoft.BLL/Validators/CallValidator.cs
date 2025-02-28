using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Validators;

public static class CallValidator
{
    public static List<string> Validate(Call call)
    {
        var errors = new List<string>();
        
        if (call.ScheduledDate == default)
        {
            errors.Add("ScheduledDate is required.");
        }
        else if (call.ScheduledDate < DateTime.UtcNow)
        {
            errors.Add("ScheduledDate cannot be in the past.");
        }
        
        if (call.Status != null && !Enum.IsDefined(typeof(CallStatus), call.Status))
        {
            errors.Add("Invalid CallStatus.");
        }

        return errors;
    }
}