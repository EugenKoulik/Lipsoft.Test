namespace Lipsoft.Data.Models;

public class CreditApplicationFilter
{
    /// <summary>
    /// Получает или устанавливает фильтр по цели кредита для заявок на кредит.
    /// </summary>
    public LoanPurpose? LoanPurpose { get; set; } 

    /// <summary>
    /// Получает или устанавливает фильтр по ID кредитного продукта для заявок на кредит.
    /// </summary>
    public long? CreditProductId { get; set; }

    /// <summary>
    /// Получает или устанавливает фильтр по минимальной сумме кредита для заявок на кредит.
    /// </summary>
    public decimal? MinLoanAmount { get; set; } 

    /// <summary>
    /// Получает или устанавливает фильтр по максимальной сумме кредита для заявок на кредит.
    /// </summary>
    public decimal? MaxLoanAmount { get; set; } 

    /// <summary>
    /// Получает или устанавливает размер страницы (количество записей на странице) для пагинации.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Получает или устанавливает смещение (количество записей для пропуска) для пагинации.
    /// </summary>
    public int Offset { get; set; } 
}