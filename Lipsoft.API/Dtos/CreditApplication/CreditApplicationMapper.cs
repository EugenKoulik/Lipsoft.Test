using Lipsoft.Data.Models;

namespace Lipsoft.API.Dtos.CreditApplication;

public class CreditApplicationMapper
{
    public static CreditApplicationModel ToCreditApplicationModel(CreditApplicationDto dto)
    {
        return  new CreditApplicationModel(
            dto.Id,
            dto.LoanPurpose,
            dto.LoanAmount,
            dto.ClientIncome,
            dto.CreditProductId
        );
    }
}