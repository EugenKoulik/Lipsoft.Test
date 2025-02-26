namespace Lipsoft.BLL.Models;

public record CreditApplicationModel(
    int Id,
    LoanPurpose? LoanPurpose,
    decimal LoanAmount,
    decimal ClientIncome,
    int CreditProductId
    );

public enum LoanPurpose
{
    
}