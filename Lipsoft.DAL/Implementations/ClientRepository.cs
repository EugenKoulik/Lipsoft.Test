using System.Runtime.CompilerServices;
using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;
using Microsoft.Data.SqlClient;

namespace Lipsoft.Data.Implementations;

public class ClientRepository : IClientRepository
{
    private readonly string? _connectionString;

    public ClientRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async IAsyncEnumerable<Client> GetAllClientsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
        
            var command = new SqlCommand("SELECT * FROM Clients", connection);
            var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                yield return new Client
                {
                    Id = (long)reader["Id"],
                    FullName = reader["FullName"].ToString(),
                    Age = (int)reader["Age"],
                    Workplace = reader["Workplace"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
            }
        }
    }
    
    public async Task<Client?> GetClientByIdAsync(long id, CancellationToken cancellationToken)
    {
        Client? client = null;
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
            var command = new SqlCommand("SELECT * FROM Clients WHERE Id = @Id", connection);
            
            command.Parameters.AddWithValue("@Id", id);
            
            var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

            if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                client = new Client
                {
                    Id = (long)reader["Id"],
                    FullName = reader["FullName"].ToString(),
                    Age = (int)reader["Age"],
                    Workplace = reader["Workplace"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
            }
        }
        return client;
    }
    
    public async Task<long> AddClientAsync(Client client, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
            var command = new SqlCommand(
                "INSERT INTO Clients (FullName, Age, Workplace, Phone) VALUES (@FullName, @Age, @Workplace, @Phone); SELECT SCOPE_IDENTITY();", 
                connection);
            
            command.Parameters.AddWithValue("@FullName", client.FullName);
            command.Parameters.AddWithValue("@Age", client.Age);
            command.Parameters.AddWithValue("@Workplace", client.Workplace);
            command.Parameters.AddWithValue("@Phone", client.Phone);
            
            var newId = Convert.ToDouble(await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false));
            
            return (long)newId;
        }
    }
    
    public async Task UpdateClientAsync(Client client, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
            var command = new SqlCommand(
                "UPDATE Clients SET FullName = @FullName, Age = @Age, Workplace = @Workplace, Phone = @Phone WHERE Id = @Id", 
                connection);
            
            command.Parameters.AddWithValue("@Id", client.Id);
            command.Parameters.AddWithValue("@FullName", client.FullName);
            command.Parameters.AddWithValue("@Age", client.Age);
            command.Parameters.AddWithValue("@Workplace", client.Workplace);
            command.Parameters.AddWithValue("@Phone", client.Phone);

            await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        }
    }
    
    public async Task DeleteClientAsync(long id, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            
            var command = new SqlCommand("DELETE FROM Clients WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}