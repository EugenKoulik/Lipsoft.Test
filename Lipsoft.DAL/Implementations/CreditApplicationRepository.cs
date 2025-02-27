using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;
using Microsoft.Data.SqlClient;

namespace Lipsoft.Data.Implementations;

public class CreditApplicationRepository : ICreditApplicationRepository
{
    private readonly string? _connectionString;

    public CreditApplicationRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<(IEnumerable<CreditApplication> CreditApplications, int TotalCount)> GetCreditApplicationsAsync(
        LoanPurpose? loanPurpose = null,
        long? creditProductId = null,
        decimal? minLoanAmount = null,
        decimal? maxLoanAmount = null,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            var countQuery = @"
                SELECT COUNT(*)
                FROM CreditApplications
                WHERE (@LoanPurpose IS NULL OR LoanPurpose = @LoanPurpose)
                  AND (@CreditProductId IS NULL OR CreditProductId = @CreditProductId)
                  AND (@MinLoanAmount IS NULL OR LoanAmount >= @MinLoanAmount)
                  AND (@MaxLoanAmount IS NULL OR LoanAmount <= @MaxLoanAmount)";

            var countCommand = new SqlCommand(countQuery, connection);
            
            countCommand.Parameters.AddWithValue("@LoanPurpose", loanPurpose ?? (object)DBNull.Value);
            countCommand.Parameters.AddWithValue("@CreditProductId", creditProductId ?? (object)DBNull.Value);
            countCommand.Parameters.AddWithValue("@MinLoanAmount", minLoanAmount ?? (object)DBNull.Value);
            countCommand.Parameters.AddWithValue("@MaxLoanAmount", maxLoanAmount ?? (object)DBNull.Value);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false));

            var query = @"
                SELECT Id, LoanPurpose, LoanAmount, ClientIncome, CreditProductId
                FROM CreditApplications
                WHERE (@LoanPurpose IS NULL OR LoanPurpose = @LoanPurpose)
                  AND (@CreditProductId IS NULL OR CreditProductId = @CreditProductId)
                  AND (@MinLoanAmount IS NULL OR LoanAmount >= @MinLoanAmount)
                  AND (@MaxLoanAmount IS NULL OR LoanAmount <= @MaxLoanAmount)
                ORDER BY Id
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LoanPurpose", loanPurpose.ToString() ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CreditProductId", creditProductId ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@MinLoanAmount", minLoanAmount ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@MaxLoanAmount", maxLoanAmount ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
            command.Parameters.AddWithValue("@PageSize", pageSize);

            var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

            var creditApplications = new List<CreditApplication>();
            
            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                creditApplications.Add(new CreditApplication
                {
                    Id = (long)reader["Id"],
                    LoanPurpose = reader["LoanPurpose"] as LoanPurpose?,
                    LoanAmount = (decimal)reader["LoanAmount"],
                    ClientIncome = (decimal)reader["ClientIncome"],
                    CreditProductId = (long)reader["CreditProductId"]
                });
            }

            return (creditApplications, totalCount);
        }
    }
    
    public async Task<CreditApplication?> GetCreditApplicationByIdAsync(long id, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            var query = "SELECT Id, LoanPurpose, LoanAmount, ClientIncome, CreditProductId FROM CreditApplications WHERE Id = @Id";
            
            var command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@Id", id);

            var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

            if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                return new CreditApplication
                {
                    Id = (long)reader["Id"],
                    LoanPurpose = reader["LoanPurpose"] as LoanPurpose?,
                    LoanAmount = (decimal)reader["LoanAmount"],
                    ClientIncome = (decimal)reader["ClientIncome"],
                    CreditProductId = (long)reader["CreditProductId"]
                };
            }

            return null;
        }
    }

    public async Task<long> AddCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            var query = @"
                INSERT INTO CreditApplications (LoanPurpose, LoanAmount, ClientIncome, CreditProductId)
                VALUES (@LoanPurpose, @LoanAmount, @ClientIncome, @CreditProductId);
                SELECT SCOPE_IDENTITY();";

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LoanPurpose", creditApplication.LoanPurpose ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@LoanAmount", creditApplication.LoanAmount);
            command.Parameters.AddWithValue("@ClientIncome", creditApplication.ClientIncome);
            command.Parameters.AddWithValue("@CreditProductId", creditApplication.CreditProductId);

            var newId = Convert.ToInt64(await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false));
            
            return newId;
        }
    }

    public async Task UpdateCreditApplicationAsync(CreditApplication creditApplication, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            var query = @"
                UPDATE CreditApplications
                SET LoanPurpose = @LoanPurpose,
                    LoanAmount = @LoanAmount,
                    ClientIncome = @ClientIncome,
                    CreditProductId = @CreditProductId
                WHERE Id = @Id";

            var command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@Id", creditApplication.Id);
            command.Parameters.AddWithValue("@LoanPurpose", creditApplication.LoanPurpose ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@LoanAmount", creditApplication.LoanAmount);
            command.Parameters.AddWithValue("@ClientIncome", creditApplication.ClientIncome);
            command.Parameters.AddWithValue("@CreditProductId", creditApplication.CreditProductId);

            await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public async Task DeleteCreditApplicationAsync(long id, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            var query = "DELETE FROM CreditApplications WHERE Id = @Id";
            
            var command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}