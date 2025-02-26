namespace Lipsoft.API.Dtos.CreditProduct;

public record CreditProductDto(
    int Id,
    string ProductName,
    decimal InterestRate
);