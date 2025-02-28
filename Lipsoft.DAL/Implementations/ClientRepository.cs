using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Lipsoft.Data.Implementations;

public class ClientRepository(IOptions<DbSettings> dbSettings) : IClientRepository
{
    private readonly string? _connectionString = dbSettings.Value.ConnectionString;
    
    public async Task<Client?> GetClientByIdAsync(long id, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var command = new SqlCommand(
            "SELECT Id, FullName, Age, Workplace, Phone FROM Clients WHERE Id = @Id", 
            connection
        );

        command.Parameters.AddWithValue("@Id", id);

        var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (await reader.ReadAsync(cancellationToken))
        {
            return new Client
            {
                Id = (long)reader["Id"],
                FullName = reader["FullName"].ToString(),
                Age = (int)reader["Age"],
                Workplace = reader["Workplace"].ToString(),
                Phone = reader["Phone"].ToString()
            };
        }

        return null;
    }
    
    public async Task<long> AddClientAsync(Client client, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
            
        var command = new SqlCommand(
            "INSERT INTO Clients (FullName, Age, Workplace, Phone) " +
            "VALUES (@FullName, @Age, @Workplace, @Phone); SELECT SCOPE_IDENTITY();", 
            connection);
            
        command.Parameters.AddWithValue("@FullName", client.FullName);
        command.Parameters.AddWithValue("@Age", client.Age);
        command.Parameters.AddWithValue("@Workplace", client.Workplace);
        command.Parameters.AddWithValue("@Phone", client.Phone);
            
        var newId = Convert.ToDouble(await command.ExecuteScalarAsync(cancellationToken));
            
        return (long)newId;
    }
    
    public async Task UpdateClientAsync(Client client, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
            
        var command = new SqlCommand(
            "UPDATE Clients SET FullName = @FullName, Age = @Age, Workplace = @Workplace, Phone = @Phone WHERE Id = @Id", 
            connection);
            
        command.Parameters.AddWithValue("@Id", client.Id);
        command.Parameters.AddWithValue("@FullName", client.FullName);
        command.Parameters.AddWithValue("@Age", client.Age);
        command.Parameters.AddWithValue("@Workplace", client.Workplace);
        command.Parameters.AddWithValue("@Phone", client.Phone);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
    
    public async Task DeleteClientAsync(long id, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
            
        var command = new SqlCommand("DELETE FROM Clients WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}