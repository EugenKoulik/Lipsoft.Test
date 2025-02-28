namespace Lipsoft.Data.Models;

public class CreditApplication
{
    /// <summary>
    /// Unique identifier for the credit application.
    /// Used to uniquely identify the application in the system.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The purpose of the loan.
    /// Describes the reason for the loan, e.g., "Home Improvement," "Car Purchase."
    /// Can be null if the purpose is not specified.
    /// </summary>
    public LoanPurpose? LoanPurpose { get; set; }

    /// <summary>
    /// The requested loan amount.
    /// Represents the amount of money the client is applying for.
    /// </summary>
    public decimal LoanAmount { get; set; }

    /// <summary>
    /// The client's income.
    /// Represents the client's monthly or annual income, depending on the context.
    /// </summary>
    public decimal ClientIncome { get; set; }

    /// <summary>
    /// The identifier of the credit product.
    /// Links the application to a specific credit product offered by the system.
    /// </summary>
    public long CreditProductId { get; set; }
}

