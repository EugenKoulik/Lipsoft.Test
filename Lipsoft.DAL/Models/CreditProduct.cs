namespace Lipsoft.Data.Models;

public record CreditProduct
{
    public long Id { get; set; }
    public string? ProductName { get; set; }
    public decimal InterestRate { get; set; }
}