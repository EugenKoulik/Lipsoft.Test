namespace Lipsoft.API.Dtos.Requests.CreditProduct;

public record AddCreditProductDto
{
    public string? ProductName { get; set; }
    public decimal InterestRate { get; set; }
}

public static class AddCreditProductDtoExtensions
{
    public static Data.Models.CreditProduct ToCreditProduct(this AddCreditProductDto dto)
    {
        return new Data.Models.CreditProduct
        {
            ProductName = dto.ProductName,
            InterestRate = dto.InterestRate
        };
    }
}