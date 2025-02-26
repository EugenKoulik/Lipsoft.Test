using Lipsoft.BLL.Models;

namespace Lipsoft.API.Dtos.Call;

public class CallMapper
{
    public static CallModel ToCallModel(CallDto dto)
    {
        return new CallModel(
            dto.Id,
            dto.ScheduledDate,
            dto.CallResult,
            dto.IsProcessed
        );
    }
    
    public static CallDto FromCallModel(CallModel model)
    {
        return new CallDto(
            model.Id,
            model.ScheduledDate,
            model.CallResult,
            model.IsProcessed
        );
    }
}