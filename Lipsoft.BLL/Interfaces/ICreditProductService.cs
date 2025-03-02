using Lipsoft.BLL.Infrastructure;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

/// <summary>
/// Определяет интерфейс сервиса для выполнения операций, связанных с кредитными продуктами.
/// </summary>
public interface ICreditProductService
{
    /// <summary>
    /// Получает конкретный кредитный продукт по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор кредитного продукта.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий кредитный продукт, или результат неудачи, если кредитный продукт не найден.</returns>
    Task<Result<CreditProduct?>> GetCreditProductByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию кредитных продуктов с поддержкой пагинации.
    /// </summary>
    /// <param name="offset">Начальный индекс кредитных продуктов для получения.</param>
    /// <param name="size">Количество кредитных продуктов для получения.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий коллекцию кредитных продуктов, или результат неудачи, если операция не удалась.</returns>
    Task<Result<IAsyncEnumerable<CreditProduct>>> GetCreditProductsAsync(int offset, int size, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новый кредитный продукт в систему.
    /// </summary>
    /// <param name="creditProduct">Кредитный продукт, который нужно добавить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий ID нового кредитного продукта, или результат неудачи, если операция не удалась.</returns>
    Task<Result<long>> AddCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет существующий кредитный продукт в системе.
    /// </summary>
    /// <param name="creditProduct">Кредитный продукт с обновленной информацией.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий обновленный кредитный продукт, или результат неудачи, если операция не удалась.</returns>
    Task<Result<CreditProduct?>> UpdateCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет кредитный продукт по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор кредитного продукта для удаления.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, который указывает, была ли операция удаления успешной, или результат неудачи, если операция не удалась.</returns>
    Task<Result<bool>> DeleteCreditProductAsync(long id, CancellationToken cancellationToken);
}