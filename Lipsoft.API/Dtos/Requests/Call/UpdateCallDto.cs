using Lipsoft.Data.Models;

namespace Lipsoft.API.Dtos.Requests.Call;

public record UpdateCallDto
{
    public DateTime ScheduledDate { get; set; } 
    public string? CallResult { get; set; }
    public CallStatus Status { get; set; }
}

public static class UpdateCallDtoExtensions
{
    public static Data.Models.Call ToCall(this UpdateCallDto dto, long id)
    {
        return new Data.Models.Call
        {
            Id = id,
            ScheduledDate = dto.ScheduledDate,
            CallResult = dto.CallResult,
            Status = dto.Status
        };
    }
}