using Lipsoft.Data.Models;

namespace Lipsoft.API.Dtos.Requests.Call;

public record AddCallDto
{
    public DateTime ScheduledDate { get; set; } 
    public string? CallResult { get; set; }
    public CallStatus Status { get; set; }
}

public static class AddCallDtoExtensions
{
    public static Data.Models.Call ToCall(this AddCallDto dto)
    {
        return new Data.Models.Call
        {
            ScheduledDate = dto.ScheduledDate,
            CallResult = dto.CallResult,
            Status = dto.Status
        };
    }
}