using Lipsoft.Data.Models;

namespace Lipsoft.API.Dtos.Requests.CreditApplication;

public record AddCreditApplicationDto
{
    public LoanPurpose? LoanPurpose { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal ClientIncome { get; set; }
    public long CreditProductId { get; set; }
}

public static class AddCreditApplicationDtoExtensions
{
    public static Data.Models.CreditApplication ToCreditApplication(this AddCreditApplicationDto dto)
    {
        return new Data.Models.CreditApplication
        {
            LoanPurpose = dto.LoanPurpose,
            LoanAmount = dto.LoanAmount,
            ClientIncome = dto.ClientIncome,
            CreditProductId = dto.CreditProductId
        };
    }
}


