using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;
using Microsoft.Data.SqlClient;

namespace Lipsoft.Data.Implementations;

public class CallRepository : ICallRepository
{
    private readonly string? _connectionString;

    public CallRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<(IEnumerable<Call> Calls, int TotalCount)> GetAllCallsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
            var countCommand = new SqlCommand("SELECT COUNT(*) FROM Calls", connection);
            
            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false));
            
            var query = @"
                SELECT Id, ScheduledDate, CallResult, Status
                FROM Calls
                ORDER BY Id
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
            command.Parameters.AddWithValue("@PageSize", pageSize);

            var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

            var calls = new List<Call>();
            
            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                var statusString = reader["Status"] as string;

                var status = Enum.TryParse(statusString, true, out CallStatus parsedStatus) ? parsedStatus : CallStatus.Unknown;

                calls.Add(new Call
                {
                    Id = (long)reader["Id"],
                    ScheduledDate = (DateTime)reader["ScheduledDate"],
                    CallResult = reader["CallResult"] as string,
                    Status = status
                });
            }

            return (calls, totalCount);
        }
    }

    public async Task<Call?> GetCallByIdAsync(long id, CancellationToken cancellationToken)
    {
        Call? call = null;
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
            var command = new SqlCommand("SELECT * FROM Calls WHERE Id = @Id", connection);
            
            command.Parameters.AddWithValue("@Id", id);
            
            var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

            if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                var statusString = reader["Status"] as string;

                var status = Enum.TryParse(statusString, true, out CallStatus parsedStatus) ? parsedStatus : CallStatus.Unknown;
                
                call = new Call
                {
                    Id = (long)reader["Id"],
                    ScheduledDate = (DateTime)reader["ScheduledDate"],
                    CallResult = reader["CallResult"] as string,
                    Status = status
                };
            }
        }
        return call;
    }
    
    public async Task<long> AddCallAsync(Call call, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
            var command = new SqlCommand(
                "INSERT INTO Calls (ScheduledDate, CallResult, Status) VALUES (@ScheduledDate, @CallResult, @Status); SELECT SCOPE_IDENTITY();", 
                connection);
            
            command.Parameters.AddWithValue("@ScheduledDate", call.ScheduledDate);
            command.Parameters.AddWithValue("@CallResult", call.CallResult ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Status", call.Status.ToString());
            
            var newId = Convert.ToDouble(await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false));
            
            return (long)newId;
        }
    }
    
    public async Task UpdateCallAsync(Call call, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
            var command = new SqlCommand(
                "UPDATE Calls SET ScheduledDate = @ScheduledDate, CallResult = @CallResult, Status = @Status WHERE Id = @Id", 
                connection);
            
            command.Parameters.AddWithValue("@Id", call.Id);
            command.Parameters.AddWithValue("@ScheduledDate", call.ScheduledDate);
            command.Parameters.AddWithValue("@CallResult", call.CallResult ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Status", call.Status.ToString());

            await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        }
    }
    
    public async Task DeleteCallAsync(long id, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
            var command = new SqlCommand("DELETE FROM Calls WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}