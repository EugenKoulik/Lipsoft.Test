using Lipsoft.BLL.Models;

namespace Lipsoft.API.Dtos.CreditProduct;

public class CreditProductMapper
{
    public static CreditProductModel ToCreditProductModel(CreditProductDto dto)
    {
        return  new CreditProductModel(
            dto.Id,
            dto.ProductName,
            dto.InterestRate
        );
    }
}