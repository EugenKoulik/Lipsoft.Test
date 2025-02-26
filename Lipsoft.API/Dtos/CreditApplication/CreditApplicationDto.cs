using Lipsoft.Data.Models;

namespace Lipsoft.API.Dtos.CreditApplication;

public record CreditApplicationDto(
    int Id,
    LoanPurpose? LoanPurpose,
    decimal LoanAmount,
    decimal ClientIncome,
    int CreditProductId
);
