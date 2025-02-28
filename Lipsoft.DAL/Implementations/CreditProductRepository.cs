using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Lipsoft.Data.Implementations;

public class CreditProductRepository(IOptions<DbSettings> dbSettings) : ICreditProductRepository
{
    private readonly string? _connectionString = dbSettings.Value.ConnectionString;

    public async IAsyncEnumerable<CreditProduct> GetCreditProductsAsync(int offset, int size, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = """
                        SELECT Id, ProductName, InterestRate
                        FROM CreditProducts
                        ORDER BY Id
                        OFFSET @Offset ROWS
                        FETCH NEXT @Size ROWS ONLY
                    """;

        var command = new SqlCommand(query, connection);
        
        command.Parameters.AddWithValue("@Offset", offset);
        command.Parameters.AddWithValue("@Size", size);

        var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            yield return new CreditProduct
            {
                Id = (long)reader["Id"],
                ProductName = reader["ProductName"].ToString(),
                InterestRate = (decimal)reader["InterestRate"]
            };
        }
    }

    public async Task<CreditProduct?> GetCreditProductByIdAsync(long id, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = "SELECT Id, ProductName, InterestRate FROM CreditProducts WHERE Id = @Id";
        var command = new SqlCommand(query, connection);
            
        command.Parameters.AddWithValue("@Id", id);

        var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (await reader.ReadAsync(cancellationToken))
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

    public async Task<long> AddCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = """
                        INSERT INTO CreditProducts (ProductName, InterestRate)
                        VALUES (@ProductName, @InterestRate);
                        SELECT SCOPE_IDENTITY();
                    """;

        var command = new SqlCommand(query, connection);
            
        command.Parameters.AddWithValue("@ProductName", creditProduct.ProductName);
        command.Parameters.AddWithValue("@InterestRate", creditProduct.InterestRate);

        var newId = Convert.ToInt64(await command.ExecuteScalarAsync(cancellationToken));

        return newId;
    }

    public async Task UpdateCreditProductAsync(CreditProduct creditProduct, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = """
                        UPDATE CreditProducts
                        SET ProductName = @ProductName,
                        InterestRate = @InterestRate
                        WHERE Id = @Id
                    """;

        var command = new SqlCommand(query, connection);
            
        command.Parameters.AddWithValue("@Id", creditProduct.Id);
        command.Parameters.AddWithValue("@ProductName", creditProduct.ProductName);
        command.Parameters.AddWithValue("@InterestRate", creditProduct.InterestRate);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteCreditProductAsync(long id, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = "DELETE FROM CreditProducts WHERE Id = @Id";
            
        var command = new SqlCommand(query, connection);
            
        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}