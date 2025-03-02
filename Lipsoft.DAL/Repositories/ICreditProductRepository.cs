using Lipsoft.Data.Models;

namespace Lipsoft.Data.Repositories;

/// <summary>
/// Определяет контракт для репозитория, ответственного за управление данными кредитных продуктов.
/// </summary>
public interface ICreditProductRepository
{
    /// <summary>
    /// Асинхронно извлекает список кредитных продуктов с поддержкой пагинации.
    /// </summary>
    /// <param name="offset">Количество элементов, которые нужно пропустить (смещение) для пагинации.</param>
    /// <param name="size">Количество элементов для извлечения (размер) при пагинации.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с результатом в виде <see cref="IAsyncEnumerable{CreditProduct}"/>.</returns>
    IAsyncEnumerable<CreditProduct> GetCreditProductsAsync(int offset, int size, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно извлекает конкретный кредитный продукт по его ID.
    /// </summary>
    /// <param name="id">ID кредитного продукта, который нужно извлечь.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с результатом в виде <see cref="CreditProduct"/> или null, если не найдено.</returns>
    Task<CreditProduct?> GetCreditProductByIdAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно добавляет новый кредитный продукт в базу данных.
    /// </summary>
    /// <param name="creditProduct">Объект <see cref="CreditProduct"/>, который нужно добавить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с ID нового кредитного продукта в качестве результата.</returns>
    Task<long> AddCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно обновляет существующий кредитный продукт в базе данных.
    /// </summary>
    /// <param name="creditProduct">Объект <see cref="CreditProduct"/>, содержащий обновленные данные.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task UpdateCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно удаляет конкретный кредитный продукт по его ID.
    /// </summary>
    /// <param name="id">ID кредитного продукта, который нужно удалить.</param>
    /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task DeleteCreditProductAsync(long id, CancellationToken cancellationToken);
}