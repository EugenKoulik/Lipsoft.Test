namespace Lipsoft.Data.Models;

public class CreditApplicationFilter
{
    /// <summary>
    /// Gets or sets the loan purpose filter for the credit applications.
    /// </summary>
    public LoanPurpose? LoanPurpose { get; set; } 

    /// <summary>
    /// Gets or sets the credit product ID filter for the credit applications.
    /// </summary>
    public long? CreditProductId { get; set; }

    /// <summary>
    /// Gets or sets the minimum loan amount filter for the credit applications.
    /// </summary>
    public decimal? MinLoanAmount { get; set; } 

    /// <summary>
    /// Gets or sets the maximum loan amount filter for the credit applications.
    /// </summary>
    public decimal? MaxLoanAmount { get; set; } 

    /// <summary>
    /// Gets or sets the size of the page (number of records per page) for pagination.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Gets or sets the offset (number of records to skip) for pagination.
    /// </summary>
    public int Offset { get; set; } 
}