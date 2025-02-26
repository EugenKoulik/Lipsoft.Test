using Lipsoft.Data.Models;
using Lipsoft.Data.Repositories;
using Microsoft.Data.SqlClient;

namespace Lipsoft.Data.Implementations;

public class ClientRepository : IClientRepository
{
    private readonly string _connectionString;

    public ClientRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<ClientModel>> GetAllClientsAsync()
    {
        var clients = new List<ClientModel>();
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync().ConfigureAwait(false);
            
            var command = new SqlCommand("SELECT * FROM Clients", connection);
            var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            while (await reader.ReadAsync().ConfigureAwait(false))
            {
                clients.Add(new ClientModel
                {
                    Id = (int)reader["Id"],
                    FullName = reader["FullName"].ToString(),
                    Age = (int)reader["Age"],
                    Workplace = reader["Workplace"].ToString(),
                    Phone = reader["Phone"].ToString()
                });
            }
        }
        return clients;
    }
    
    public async Task<ClientModel?> GetClientByIdAsync(int id)
    {
        ClientModel? client = null;
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync().ConfigureAwait(false);
            
            var command = new SqlCommand("SELECT * FROM Clients WHERE Id = @Id", connection);
            
            command.Parameters.AddWithValue("@Id", id);
            
            var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            if (await reader.ReadAsync().ConfigureAwait(false))
            {
                client = new ClientModel
                {
                    Id = (int)reader["Id"],
                    FullName = reader["FullName"].ToString(),
                    Age = (int)reader["Age"],
                    Workplace = reader["Workplace"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
            }
        }
        return client;
    }
    
    public async Task AddClientAsync(ClientModel client)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync().ConfigureAwait(false);
            
            var command = new SqlCommand(
                "INSERT INTO Clients (FullName, Age, Workplace, Phone) VALUES (@FullName, @Age, @Workplace, @Phone); SELECT SCOPE_IDENTITY();", 
                connection);
            
            command.Parameters.AddWithValue("@FullName", client.FullName);
            command.Parameters.AddWithValue("@Age", client.Age);
            command.Parameters.AddWithValue("@Workplace", client.Workplace);
            command.Parameters.AddWithValue("@Phone", client.Phone);
            
            var newId = Convert.ToInt32(await command.ExecuteScalarAsync().ConfigureAwait(false));
            
            client.Id = newId;
        }
    }
    
    public async Task UpdateClientAsync(ClientModel client)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync().ConfigureAwait(false);
            
            var command = new SqlCommand(
                "UPDATE Clients SET FullName = @FullName, Age = @Age, Workplace = @Workplace, Phone = @Phone WHERE Id = @Id", 
                connection);
            
            command.Parameters.AddWithValue("@Id", client.Id);
            command.Parameters.AddWithValue("@FullName", client.FullName);
            command.Parameters.AddWithValue("@Age", client.Age);
            command.Parameters.AddWithValue("@Workplace", client.Workplace);
            command.Parameters.AddWithValue("@Phone", client.Phone);

            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
    
    public async Task DeleteClientAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync().ConfigureAwait(false);
            
            var command = new SqlCommand("DELETE FROM Clients WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}