namespace Lipsoft.BLL.Models;

public record CallModel(
    int Id,
    DateTime ScheduledDate,
    string CallResult,
    bool IsProcessed
);