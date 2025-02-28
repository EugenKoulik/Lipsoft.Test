namespace Lipsoft.Data.Models;

public class CreditProduct
{
    /// <summary>
    /// Unique identifier for the credit product.
    /// Used to uniquely identify the product in the system.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The name of the credit product.
    /// Describes the type or name of the credit product, e.g., "Personal Loan," "Mortgage."
    /// Can be null if the name is not specified.
    /// </summary>
    public string? ProductName { get; set; }

    /// <summary>
    /// The interest rate associated with the credit product.
    /// Represents the annual interest rate as a percentage.
    /// </summary>
    public decimal InterestRate { get; set; }
}