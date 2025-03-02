using Lipsoft.BLL.Infrastructure;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

/// <summary>
/// Определяет интерфейс сервиса для выполнения операций, связанных с заявками на кредиты.
/// </summary>
public interface ICreditApplicationService
{
    /// <summary>
    /// Получает коллекцию заявок на кредит, основанную на предоставленных критериях фильтрации.
    /// </summary>
    /// <param name="filter">Критерии фильтрации, которые необходимо применить при получении заявок на кредит.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий коллекцию заявок на кредит, или результат неудачи, если операция не удалась.</returns>
    Task<Result<IAsyncEnumerable<CreditApplication>>> GetCreditApplicationsAsync(CreditApplicationFilter filter, CancellationToken cancellationToken);

    /// <summary>
    /// Получает конкретную заявку на кредит по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор заявки на кредит.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий заявку на кредит, или результат неудачи, если заявка не найдена.</returns>
    Task<Result<CreditApplication?>> GetCreditApplicationByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новую заявку на кредит в систему.
    /// Также автоматически создается звонок, запланированный на будущую дату, когда добавляется заявка на кредит.
    /// </summary>
    /// <param name="creditApplication">Заявка на кредит, которую нужно добавить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий ID новой заявки на кредит, или результат неудачи, если операция не удалась.</returns>
    Task<Result<long>> AddCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет существующую заявку на кредит в системе.
    /// </summary>
    /// <param name="creditApplication">Заявка на кредит с обновленной информацией.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий обновленную заявку на кредит, или результат неудачи, если операция не удалась.</returns>
    Task<Result<CreditApplication?>> UpdateCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет заявку на кредит по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор заявки на кредит для удаления.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, который указывает, была ли операция удаления успешной, или результат неудачи, если операция не удалась.</returns>
    Task<Result<bool>> DeleteCreditApplicationAsync(long id, CancellationToken cancellationToken);
}