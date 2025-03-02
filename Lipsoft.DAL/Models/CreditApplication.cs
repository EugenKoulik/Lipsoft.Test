namespace Lipsoft.Data.Models;

public class CreditApplication
{
    /// <summary>
    /// Уникальный идентификатор заявки на кредит.
    /// Используется для уникальной идентификации заявки в системе.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Цель кредита.
    /// Описывает причину получения кредита, например: "Ремонт дома," "Покупка автомобиля."
    /// Может быть null, если цель не указана.
    /// </summary>
    public LoanPurpose? LoanPurpose { get; set; }

    /// <summary>
    /// Запрашиваемая сумма кредита.
    /// Представляет сумму денег, на которую клиент подает заявку.
    /// </summary>
    public decimal LoanAmount { get; set; }

    /// <summary>
    /// Доход клиента.
    /// Представляет ежемесячный или годовой доход клиента, в зависимости от контекста.
    /// </summary>
    public decimal ClientIncome { get; set; }

    /// <summary>
    /// Идентификатор кредитного продукта.
    /// Связывает заявку с конкретным кредитным продуктом, предлагаемым системой.
    /// </summary>
    public long CreditProductId { get; set; }
}

