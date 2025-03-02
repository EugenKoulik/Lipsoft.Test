namespace Lipsoft.Data.Models;

/// <summary>
/// Представляет возможные статусы звонка.
/// </summary>
public enum CallStatus
{
    /// <summary>
    /// Звонок запланирован.
    /// </summary>
    Scheduled,

    /// <summary>
    /// Звонок в процессе.
    /// </summary>
    InProgress,

    /// <summary>
    /// Звонок завершен.
    /// </summary>
    Completed,

    /// <summary>
    /// Звонок отменен.
    /// </summary>
    Cancelled
}