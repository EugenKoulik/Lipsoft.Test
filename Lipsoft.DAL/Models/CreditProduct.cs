namespace Lipsoft.Data.Models;

public class CreditProduct
{
    /// <summary>
    /// Уникальный идентификатор кредитного продукта.
    /// Используется для уникальной идентификации продукта в системе.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Название кредитного продукта.
    /// Описывает тип или название кредитного продукта, например: "Личный кредит," "Ипотека."
    /// Может быть null, если название не указано.
    /// </summary>
    public string? ProductName { get; set; }

    /// <summary>
    /// Процентная ставка, связанная с кредитным продуктом.
    /// Представляет годовую процентную ставку в процентах.
    /// </summary>
    public decimal InterestRate { get; set; }
}