using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;
using Microsoft.Data.SqlClient;

namespace Lipsoft.Data.Implementations;

public class CreditProductRepository : ICreditProductRepository
{
    private readonly string? _connectionString;

    public CreditProductRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<(IEnumerable<CreditProduct> CreditProducts, int TotalCount)> GetCreditProductsAsync(
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
            var countCommand = new SqlCommand("SELECT COUNT(*) FROM CreditProducts", connection);
            
            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false));
            
            var query = @"
                SELECT Id, ProductName, InterestRate
                FROM CreditProducts
                ORDER BY Id
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";

            var command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
            command.Parameters.AddWithValue("@PageSize", pageSize);

            var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

            var creditProducts = new List<CreditProduct>();
            
            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                creditProducts.Add(new CreditProduct
                {
                    Id = (long)reader["Id"],
                    ProductName = reader["ProductName"].ToString(),
                    InterestRate = (decimal)reader["InterestRate"]
                });
            }

            return (creditProducts, totalCount);
        }
    }

    public async Task<CreditProduct?> GetCreditProductByIdAsync(long id, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            var query = "SELECT Id, ProductName, InterestRate FROM CreditProducts WHERE Id = @Id";
            var command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@Id", id);

            var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

            if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                return new CreditProduct
                {
                    Id = (long)reader["Id"],
                    ProductName = reader["ProductName"].ToString(),
                    InterestRate = (decimal)reader["InterestRate"]
                };
            }

            return null;
        }
    }

    public async Task<long> AddCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            var query = @"
                INSERT INTO CreditProducts (ProductName, InterestRate)
                VALUES (@ProductName, @InterestRate);
                SELECT SCOPE_IDENTITY();";

            var command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@ProductName", creditProduct.ProductName);
            command.Parameters.AddWithValue("@InterestRate", creditProduct.InterestRate);

            var newId = Convert.ToInt64(await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false));

            return newId;
        }
    }

    public async Task UpdateCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            var query = @"
                UPDATE CreditProducts
                SET ProductName = @ProductName,
                    InterestRate = @InterestRate
                WHERE Id = @Id";

            var command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@Id", creditProduct.Id);
            command.Parameters.AddWithValue("@ProductName", creditProduct.ProductName);
            command.Parameters.AddWithValue("@InterestRate", creditProduct.InterestRate);

            await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public async Task DeleteCreditProductAsync(long id, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            var query = "DELETE FROM CreditProducts WHERE Id = @Id";
            
            var command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}