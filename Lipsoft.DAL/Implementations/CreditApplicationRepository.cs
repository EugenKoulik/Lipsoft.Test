using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Lipsoft.Data.Implementations;

public class CreditApplicationRepository(IOptions<DbSettings> dbSettings) : ICreditApplicationRepository
{
    private readonly string? _connectionString = dbSettings.Value.ConnectionString;

    public async IAsyncEnumerable<CreditApplication> GetCreditApplicationsAsync(CreditApplicationFilter filter, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = @"
            SELECT Id, LoanPurpose, LoanAmount, ClientIncome, CreditProductId
            FROM CreditApplications
            WHERE (@CreditProductId IS NULL OR CreditProductId = @CreditProductId)
              AND (@LoanPurpose IS NULL OR LoanPurpose = @LoanPurpose)
              AND (@MinLoanAmount IS NULL OR LoanAmount >= @MinLoanAmount)
              AND (@MaxLoanAmount IS NULL OR LoanAmount <= @MaxLoanAmount)
            ORDER BY Id
            OFFSET @Offset ROWS
            FETCH NEXT @Size ROWS ONLY";

        var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@LoanPurpose", filter.LoanPurpose?.ToString() ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@CreditProductId", filter.CreditProductId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@MinLoanAmount", filter.MinLoanAmount ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@MaxLoanAmount", filter.MaxLoanAmount ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Offset", filter.Offset);
        command.Parameters.AddWithValue("@Size", filter.Size);

        var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            yield return new CreditApplication
            {
                Id = (long)reader["Id"],
                LoanPurpose = Enum.TryParse<LoanPurpose>(reader["LoanPurpose"].ToString(), out var loanPurpose) ? loanPurpose : null,
                LoanAmount = (decimal)reader["LoanAmount"],
                ClientIncome = (decimal)reader["ClientIncome"],
                CreditProductId = (long)reader["CreditProductId"]
            };
        }
    }
    
    public async Task<CreditApplication?> GetCreditApplicationByIdAsync(long id, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = "SELECT Id, LoanPurpose, LoanAmount, ClientIncome, CreditProductId FROM CreditApplications WHERE Id = @Id";
            
        var command = new SqlCommand(query, connection);
            
        command.Parameters.AddWithValue("@Id", id);

        var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (await reader.ReadAsync(cancellationToken))
        {
            return new CreditApplication
            {
                Id = (long)reader["Id"],
                LoanPurpose = Enum.TryParse<LoanPurpose>(reader["LoanPurpose"].ToString(), out var loanPurpose) ? loanPurpose : null,
                LoanAmount = (decimal)reader["LoanAmount"],
                ClientIncome = (decimal)reader["ClientIncome"],
                CreditProductId = (long)reader["CreditProductId"]
            };
        }

        return null;
    }

    public async Task<long> AddCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = """
                        INSERT INTO CreditApplications (LoanPurpose, LoanAmount, ClientIncome, CreditProductId)
                        VALUES (@LoanPurpose, @LoanAmount, @ClientIncome, @CreditProductId);
                        SELECT SCOPE_IDENTITY();
                    """;

        var command = new SqlCommand(query, connection);
        
        command.Parameters.AddWithValue("@LoanPurpose", creditApplication.LoanPurpose.ToString());
        command.Parameters.AddWithValue("@LoanAmount", creditApplication.LoanAmount);
        command.Parameters.AddWithValue("@ClientIncome", creditApplication.ClientIncome);
        command.Parameters.AddWithValue("@CreditProductId", creditApplication.CreditProductId);

        var newId = Convert.ToInt64(await command.ExecuteScalarAsync(cancellationToken));
            
        return newId;
    }

    public async Task UpdateCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = """
                        UPDATE CreditApplications
                        SET LoanPurpose = @LoanPurpose,
                        LoanAmount = @LoanAmount,
                        ClientIncome = @ClientIncome,
                        CreditProductId = @CreditProductId
                        WHERE Id = @Id
                    """;

        var command = new SqlCommand(query, connection);
            
        command.Parameters.AddWithValue("@Id", creditApplication.Id);
        command.Parameters.AddWithValue("@LoanPurpose", creditApplication.LoanPurpose.ToString());
        command.Parameters.AddWithValue("@LoanAmount", creditApplication.LoanAmount);
        command.Parameters.AddWithValue("@ClientIncome", creditApplication.ClientIncome);
        command.Parameters.AddWithValue("@CreditProductId", creditApplication.CreditProductId);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteCreditApplicationAsync(long id, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = "DELETE FROM CreditApplications WHERE Id = @Id";
            
        var command = new SqlCommand(query, connection);
            
        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}