namespace Lipsoft.Data.Models;

public class CreditApplication
{
    public long Id { get; set; }
    public LoanPurpose? LoanPurpose { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal ClientIncome { get; set; }
    public long CreditProductId { get; set; }
}

public enum LoanPurpose
{
    Unknown,
    HomePurchase,
    CarPurchase,
    DebtConsolidation,
    Education,
    MedicalExpenses,
    BusinessInvestment,
    Travel,
    Other
}