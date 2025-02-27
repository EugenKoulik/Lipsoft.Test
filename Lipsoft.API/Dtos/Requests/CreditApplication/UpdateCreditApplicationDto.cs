using Lipsoft.Data.Models;

namespace Lipsoft.API.Dtos.Requests.CreditApplication;

public record UpdateCreditApplicationDto
{
    public LoanPurpose? LoanPurpose { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal ClientIncome { get; set; }
    public long CreditProductId { get; set; }
}

public static class UpdateCreditApplicationDtoExtensions
{
    public static Data.Models.CreditApplication ToCreditApplication(this UpdateCreditApplicationDto dto)
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