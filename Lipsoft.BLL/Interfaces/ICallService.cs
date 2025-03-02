using Lipsoft.BLL.Infrastructure;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

/// <summary>
/// Определяет интерфейс сервиса для выполнения операций, связанных с звонками.
/// </summary>
public interface ICallService
{
    /// <summary>
    /// Получает звонок по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор звонка.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий звонок, или результат неудачи, если звонок не найден.</returns>
    Task<Result<Call?>> GetCallByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает список звонков с пагинацией.
    /// </summary>
    /// <param name="offset">Количество записей для пропуска.</param>
    /// <param name="size">Количество записей для получения.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий <see cref="IAsyncEnumerable{Call}"/> звонков, или результат неудачи, если операция не удалась.</returns>
    Task<Result<IAsyncEnumerable<Call>>> GetCallsAsync(int offset, int size, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новый звонок в систему.
    /// </summary>
    /// <param name="call">Звонок, который нужно добавить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий ID нового звонка, или результат неудачи, если операция не удалась.</returns>
    Task<Result<long>> AddCallAsync(Call call, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет существующий звонок в системе.
    /// </summary>
    /// <param name="call">Звонок с обновленной информацией.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий обновленный звонок, или результат неудачи, если операция не удалась.</returns>
    Task<Result<Call?>> UpdateCallAsync(Call call, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет звонок по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор звонка для удаления.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, который указывает, была ли операция удаления успешной, или результат неудачи, если операция не удалась.</returns>
    Task<Result<bool>> DeleteCallAsync(long id, CancellationToken cancellationToken);
}