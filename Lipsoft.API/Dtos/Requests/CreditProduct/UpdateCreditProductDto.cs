namespace Lipsoft.API.Dtos.Requests.CreditProduct;

public record UpdateCreditProductDto
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public decimal InterestRate { get; set; }
}

public static class UpdateCreditProductDtoExtensions
{
    public static Data.Models.CreditProduct ToCreditProduct(this UpdateCreditProductDto dto)
    {
        return new Data.Models.CreditProduct
        {
            Id = dto.Id,
            ProductName = dto.ProductName,
            InterestRate = dto.InterestRate
        };
    }
}