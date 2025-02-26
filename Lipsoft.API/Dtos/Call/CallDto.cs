namespace Lipsoft.API.Dtos.Call;

public record CallDto(
    int Id,
    DateTime ScheduledDate,
    string CallResult,
    bool IsProcessed
);