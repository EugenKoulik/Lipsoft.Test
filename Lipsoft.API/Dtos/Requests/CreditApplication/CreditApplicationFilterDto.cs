using Lipsoft.Data.Models;

namespace Lipsoft.API.Dtos.Requests.CreditApplication;

public record CreditApplicationFilterDto
{
    public LoanPurpose? LoanPurpose { get; set; }
    public long? CreditProductId { get; set; }
    public decimal? MinLoanAmount { get; set; }
    public decimal? MaxLoanAmount { get; set; }
    public int Offset { get; set; } = 0;
    public int Size { get; set; } = 10;
}

public static class CreditApplicationFilterDtoExtensions
{
    public static CreditApplicationFilter ToCreditApplicationFilter(this CreditApplicationFilterDto dto)
    {
        return new CreditApplicationFilter
        {
            LoanPurpose = dto.LoanPurpose,
            CreditProductId = dto.CreditProductId,
            MinLoanAmount = dto.MinLoanAmount,
            MaxLoanAmount = dto.MaxLoanAmount,
            Offset = dto.Offset,
            Size = dto.Size
        };
    }
}