namespace Lipsoft.API.Dtos.Requests.CreditProduct;

public record UpdateCreditProductDto
{
    public string? ProductName { get; set; }
    public decimal InterestRate { get; set; }
}

public static class UpdateCreditProductDtoExtensions
{
    public static Data.Models.CreditProduct ToCreditProduct(this UpdateCreditProductDto dto, long id)
    {
        return new Data.Models.CreditProduct
        {
            Id = id,
            ProductName = dto.ProductName,
            InterestRate = dto.InterestRate
        };
    }
}