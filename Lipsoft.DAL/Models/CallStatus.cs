namespace Lipsoft.Data.Models;

/// <summary>
/// Represents the possible statuses of a call.
/// </summary>
public enum CallStatus
{
    /// <summary>
    /// The call is scheduled.
    /// </summary>
    Scheduled,

    /// <summary>
    /// The call is in progress.
    /// </summary>
    InProgress,

    /// <summary>
    /// The call is completed.
    /// </summary>
    Completed,

    /// <summary>
    /// The call is cancelled.
    /// </summary>
    Cancelled
}