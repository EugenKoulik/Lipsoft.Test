namespace Lipsoft.Data.Models;

public class Client
{
    /// <summary>
    /// Unique identifier for the client.
    /// Used to uniquely identify the client in the system.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The full name of the client.
    /// Contains the first name, last name, and optionally the middle name.
    /// Can be null if the full name is not provided.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// The age of the client.
    /// Represents the client's age in years.
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// The workplace of the client.
    /// Contains the name of the organization or company where the client works.
    /// Can be null if the workplace is not specified.
    /// </summary>
    public string? Workplace { get; set; }

    /// <summary>
    /// The phone number of the client.
    /// Contains the contact phone number in a string format.
    /// Can be null if the phone number is not provided.
    /// </summary>
    public string? Phone { get; set; }
}
  