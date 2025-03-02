using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

/// <summary>
/// Определяет контракт для репозитория, ответственного за управление данными клиентов.
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Асинхронно извлекает конкретного клиента по его ID.
    /// </summary>
    /// <param name="id">ID клиента, которого нужно извлечь.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с результатом в виде <see cref="Client"/> или null, если не найдено.</returns>
    Task<Client?> GetClientByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно добавляет новую запись о клиенте в базу данных.
    /// </summary>
    /// <param name="client">Объект <see cref="Client"/>, который нужно добавить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с ID нового клиента в качестве результата.</returns>
    Task<long> AddClientAsync(Client client, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно обновляет существующую запись о клиенте в базе данных.
    /// </summary>
    /// <param name="client">Объект <see cref="Client"/>, содержащий обновленные данные.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task UpdateClientAsync(Client client, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно удаляет конкретную запись о клиенте по его ID.
    /// </summary>
    /// <param name="id">ID клиента, которого нужно удалить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task DeleteClientAsync(long id, CancellationToken cancellationToken);
}