namespace Lipsoft.Data.Models;

public class Call
{
    public long Id { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string? CallResult { get; set; }
    public CallStatus Status { get; set; }
}

public enum CallStatus
{
    Unknown,
    Scheduled,  
    InProgress,  
    Completed,   
    Cancelled,   
    Missed   
}