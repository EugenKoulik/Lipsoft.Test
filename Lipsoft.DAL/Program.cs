using System.Text.Json;
using Lipsoft.Data.Migrations;

namespace Lipsoft.Data;

public class Program
{
    static void Main(string[] args)
    {
        /*var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        if (!File.Exists(appSettingsPath))
        {
            Console.WriteLine("appsettings.json not found.");
            return;
        }

        var json = File.ReadAllText(appSettingsPath);
        var appSettings = JsonSerializer.Deserialize<AppSettings>(json);

        if (appSettings == null || string.IsNullOrEmpty(appSettings.ConnectionStrings?.DefaultConnection))
        {
            Console.WriteLine("Connection string is missing in appsettings.json.");
            return;
        }

        var connectionString = appSettings.ConnectionStrings.DefaultConnection;

        var migrator = new Migrator(connectionString);

        if (args.Contains("--apply"))
        {
            Console.WriteLine("Applying migrations...");
            migrator.ApplyMigrations();
        }
        else if (args.Contains("--rollback"))
        {
            Console.WriteLine("Rolling back migrations...");
            migrator.RollbackMigrations();
        }
        else
        {
            Console.WriteLine("Please specify --apply or --rollback.");
        }*/
    }
}