using Lipsoft.BLL.Infrastructure;
using Lipsoft.Data.Models;

namespace Lipsoft.BLL.Interfaces;

/// <summary>
/// Определяет интерфейс сервиса для выполнения операций, связанных с клиентами.
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Получает клиента по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор клиента.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий клиента, или результат неудачи, если клиент не найден.</returns>
    Task<Result<Client?>> GetClientById(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет нового клиента в систему.
    /// </summary>
    /// <param name="client">Клиент, которого нужно добавить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий ID нового клиента, или результат неудачи, если операция не удалась.</returns>
    Task<Result<long>> AddClient(Client client, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет существующего клиента в системе.
    /// </summary>
    /// <param name="client">Клиент с обновленной информацией.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, содержащий обновленного клиента, или результат неудачи, если операция не удалась.</returns>
    Task<Result<Client?>> UpdateClient(Client client, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет клиента по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор клиента для удаления.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Результат <see cref="Result"/>, который указывает, была ли операция удаления успешной, или результат неудачи, если операция не удалась.</returns>
    Task<Result<bool>> DeleteClient(long id, CancellationToken cancellationToken);
}