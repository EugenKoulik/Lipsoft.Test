using Lipsoft.Data.Repositories;

namespace Lipsoft.Data;

public class DatabaseConnection : IDatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection(string connectionString)
    {
        _connectionString = connectionString;
    }
}