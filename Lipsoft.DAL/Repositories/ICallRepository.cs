using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

/// <summary>
/// Определяет контракт для репозитория, ответственного за управление данными о звонках.
/// </summary>
public interface ICallRepository
{
    /// <summary>
    /// Асинхронно извлекает список звонков с пагинацией.
    /// </summary>
    /// <param name="offset">Количество записей, которые нужно пропустить для пагинации.</param>
    /// <param name="size">Количество записей для извлечения.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Асинхронный поток сущностей <see cref="Call"/>.</returns>
    IAsyncEnumerable<Call> GetCallsAsync(int offset, int size, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно извлекает конкретный звонок по его ID.
    /// </summary>
    /// <param name="id">ID звонка, который нужно извлечь.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с результатом в виде <see cref="Call"/> или null, если не найдено.</returns>
    Task<Call?> GetCallByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно добавляет новый записанный звонок в базу данных.
    /// </summary>
    /// <param name="call">Объект <see cref="Call"/>, который нужно добавить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с ID нового звонка в качестве результата.</returns>
    Task<long> AddCallAsync(Call call, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно обновляет существующий записанный звонок в базе данных.
    /// </summary>
    /// <param name="call">Объект <see cref="Call"/>, содержащий обновленные данные.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task UpdateCallAsync(Call call, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно удаляет конкретный звонок по его ID.
    /// </summary>
    /// <param name="id">ID звонка, который нужно удалить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task DeleteCallAsync(long id, CancellationToken cancellationToken);
}