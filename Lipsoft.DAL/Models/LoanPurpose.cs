namespace Lipsoft.Data.Models;

/// <summary>
/// Представляет возможные цели кредита.
/// </summary>
public enum LoanPurpose
{
    /// <summary>
    /// Кредит предназначен для улучшения жилья.
    /// </summary>
    HomeImprovement,

    /// <summary>
    /// Кредит предназначен для покупки автомобиля.
    /// </summary>
    CarPurchase,

    /// <summary>
    /// Кредит предназначен для консолидации долгов.
    /// </summary>
    DebtConsolidation,

    /// <summary>
    /// Кредит предназначен для образовательных целей.
    /// </summary>
    Education
}