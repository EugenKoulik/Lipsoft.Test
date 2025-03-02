using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

/// <summary>
/// Определяет контракт для репозитория, ответственного за управление данными заявок на кредит.
/// </summary>
public interface ICreditApplicationRepository
{
    /// <summary>
    /// Асинхронно извлекает список заявок на кредит на основе предоставленного фильтра.
    /// </summary>
    /// <param name="filter">Критерии фильтрации, используемые для извлечения заявок на кредит.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с результатом в виде <see cref="IAsyncEnumerable{CreditApplication}"/>.</returns>
    IAsyncEnumerable<CreditApplication> GetCreditApplicationsAsync(CreditApplicationFilter filter, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно извлекает конкретную заявку на кредит по её ID.
    /// </summary>
    /// <param name="id">ID заявки на кредит, которую нужно извлечь.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с результатом в виде <see cref="CreditApplication"/> или null, если не найдено.</returns>
    Task<CreditApplication?> GetCreditApplicationByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно добавляет новую заявку на кредит в базу данных.
    /// </summary>
    /// <param name="creditApplication">Объект <see cref="CreditApplication"/>, который нужно добавить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с ID новой заявки на кредит в качестве результата.</returns>
    Task<long> AddCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно обновляет существующую заявку на кредит в базе данных.
    /// </summary>
    /// <param name="creditApplication">Объект <see cref="CreditApplication"/>, содержащий обновленные данные.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task UpdateCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно удаляет конкретную заявку на кредит по её ID.
    /// </summary>
    /// <param name="id">ID заявки на кредит, которую нужно удалить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task DeleteCreditApplicationAsync(long id, CancellationToken cancellationToken);
}