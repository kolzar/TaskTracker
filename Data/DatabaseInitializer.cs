using Dapper;
using Microsoft.Data.Sqlite;

namespace TaskTracker.Api.Data;

public class DatabaseInitializer
{
    private readonly IConfiguration _configuration;

    public DatabaseInitializer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Initialize()
    {
        using var connection = new SqliteConnection(
            _configuration.GetConnectionString("DefaultConnection"));

        connection.Execute("""
            CREATE TABLE IF NOT EXISTS Tasks (
                Id TEXT PRIMARY KEY,
                Title TEXT NOT NULL,
                Description TEXT NULL,
                Status TEXT NOT NULL,
                Priority TEXT NOT NULL,
                DueDate TEXT NULL,
                CreatedAt TEXT NOT NULL,
                UpdatedAt TEXT NULL
            );
        """);
    }
}