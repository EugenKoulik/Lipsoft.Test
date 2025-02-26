using Microsoft.Data.SqlClient;

namespace Lipsoft.Data.Migrations;

public class Migrator
{
   private readonly string _connectionString;

    public Migrator(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void ApplyMigrations()
    {
        var migrations = new List<string>
        {
            "26022025_init/Up.sql"
        };

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            
            EnsureMigrationTableExists(connection);

            foreach (var migration in migrations)
            {
                var migrationName = Path.GetFileNameWithoutExtension(migration);

                if (IsMigrationApplied(connection, migrationName))
                {
                    continue;
                }

                var scriptPath = Path.Combine("Migrations", migration);
                
                if (!File.Exists(scriptPath))
                {
                    throw new FileNotFoundException($"Migration script not found: {scriptPath}");
                }

                var script = File.ReadAllText(scriptPath);

                using (var command = new SqlCommand(script, connection))
                {
                    command.ExecuteNonQuery();
                }

                MarkMigrationAsApplied(connection, migrationName);
            }
        }
    }

    public void RollbackMigrations()
    {
        var migrations = new List<string>
        {
            "26022025_init/Down.sql", 
        };

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            foreach (var migration in migrations)
            {
                var migrationName = Path.GetFileNameWithoutExtension(migration);

                var scriptPath = Path.Combine("Migrations", migration);
                
                if (!File.Exists(scriptPath))
                {
                    throw new FileNotFoundException($"Rollback script not found: {scriptPath}");
                }

                var script = File.ReadAllText(scriptPath);

                using (var command = new SqlCommand(script, connection))
                {
                    command.ExecuteNonQuery();
                }

                MarkMigrationAsRolledBack(connection, migrationName);
            }
        }
    }

    private void EnsureMigrationTableExists(SqlConnection connection)
    {
        var createTableSql = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='__MigrationsHistory' AND xtype='U')
            CREATE TABLE __MigrationsHistory (
                MigrationId NVARCHAR(150) PRIMARY KEY,
                AppliedOn DATETIME NOT NULL DEFAULT GETDATE()
            );
        ";

        using (var command = new SqlCommand(createTableSql, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    private bool IsMigrationApplied(SqlConnection connection, string migrationName)
    {
        var checkSql = "SELECT 1 FROM __MigrationsHistory WHERE MigrationId = @MigrationId";
        
        using (var command = new SqlCommand(checkSql, connection))
        {
            command.Parameters.AddWithValue("@MigrationId", migrationName);
            return command.ExecuteScalar() != null;
        }
    }

    private void MarkMigrationAsApplied(SqlConnection connection, string migrationName)
    {
        var insertSql = "INSERT INTO __MigrationsHistory (MigrationId) VALUES (@MigrationId)";
        
        using (var command = new SqlCommand(insertSql, connection))
        {
            command.Parameters.AddWithValue("@MigrationId", migrationName);
            command.ExecuteNonQuery();
        }
    }

    private void MarkMigrationAsRolledBack(SqlConnection connection, string migrationName)
    {
        var deleteSql = "DELETE FROM __MigrationsHistory WHERE MigrationId = @MigrationId";
        
        using (var command = new SqlCommand(deleteSql, connection))
        {
            command.Parameters.AddWithValue("@MigrationId", migrationName);
            command.ExecuteNonQuery();
        }
    }
}