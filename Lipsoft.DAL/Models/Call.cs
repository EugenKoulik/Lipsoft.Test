namespace Lipsoft.Data.Models;

public class Call
{
    /// <summary>
    /// Unique identifier for the call.
    /// Used to uniquely identify the object in the system.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The date and time when the call is scheduled.
    /// Can be used for scheduling and reminders.
    /// </summary>
    public DateTime ScheduledDate { get; set; }

    /// <summary>
    /// The result of the call.
    /// Contains a textual description of the call outcome, e.g., "Agreed to meet."
    /// Can be null if the call has not been made or the result is not recorded.
    /// </summary>
    public string? CallResult { get; set; }

    /// <summary>
    /// The current status of the call.
    /// Represents the state of the call, e.g., "Scheduled," "Completed," "Cancelled."
    /// Can be null if the status is not set.
    /// </summary>
    public CallStatus? Status { get; set; }
}

