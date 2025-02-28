using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Lipsoft.Data.Implementations;

public class CallRepository(IOptions<DbSettings> dbSettings) : ICallRepository
{
    private readonly string? _connectionString = dbSettings.Value.ConnectionString;

    public async IAsyncEnumerable<Call> GetCallsAsync(int offset, int size, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = """
                        SELECT Id, ScheduledDate, CallResult, Status
                        FROM Calls
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
            yield return new Call
            {
                Id = (long)reader["Id"],
                ScheduledDate = (DateTime)reader["ScheduledDate"],
                CallResult = reader["CallResult"] as string,
                Status = Enum.TryParse<CallStatus>(reader["Status"].ToString(), out var status) ? status : null
            };
        }
    }

    public async Task<Call?> GetCallByIdAsync(long id, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        
        var command = new SqlCommand(
            "SELECT Id, ScheduledDate, CallResult, Status FROM Calls WHERE Id = @Id", 
            connection
        );
        command.Parameters.AddWithValue("@Id", id);
        
        var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (await reader.ReadAsync(cancellationToken))
        {
            return new Call
            {
                Id = (long)reader["Id"],
                ScheduledDate = (DateTime)reader["ScheduledDate"],
                CallResult = reader["CallResult"] as string,
                Status = Enum.TryParse<CallStatus>(reader["Status"].ToString(), out var status) ? status : null
            };
        }

        return null;
    }
    
    public async Task<long> AddCallAsync(Call call, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
            
        var command = new SqlCommand(
            "INSERT INTO Calls (ScheduledDate, CallResult, Status) " +
            "VALUES (@ScheduledDate, @CallResult, @Status); SELECT SCOPE_IDENTITY();", 
            connection);
            
        command.Parameters.AddWithValue("@ScheduledDate", call.ScheduledDate);
        command.Parameters.AddWithValue("@CallResult", call.CallResult ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Status", call.Status.ToString());
            
        var newId = Convert.ToDouble(await command.ExecuteScalarAsync(cancellationToken));
            
        return (long)newId;
    }
    
    public async Task UpdateCallAsync(Call call, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
        var command = new SqlCommand(
            "UPDATE Calls SET ScheduledDate = @ScheduledDate, CallResult = @CallResult, Status = @Status WHERE Id = @Id", 
            connection);
            
        command.Parameters.AddWithValue("@Id", call.Id);
        command.Parameters.AddWithValue("@ScheduledDate", call.ScheduledDate);
        command.Parameters.AddWithValue("@CallResult", call.CallResult ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Status", call.Status.ToString());

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
    
    public async Task DeleteCallAsync(long id, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
        var command = new SqlCommand("DELETE FROM Calls WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}