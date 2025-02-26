namespace Lipsoft.Data.Models;

public record CallModel(
    int Id,
    DateTime ScheduledDate,
    string CallResult,
    bool IsProcessed
);