using Dapper;
using Microsoft.Data.Sqlite;
using TaskTracker.Api.Models;

namespace TaskTracker.Api.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IConfiguration _configuration;

    public TaskRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private SqliteConnection CreateConnection()
    {
        return new SqliteConnection(
            _configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        using var connection = CreateConnection();

        var data = await connection.QueryAsync<TaskItem>(
            "SELECT * FROM Tasks ORDER BY CreatedAt DESC");
        data = data.Where(task => task.Id != Guid.Empty);
        return data;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        using var connection = CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<TaskItem>(
            "SELECT * FROM Tasks WHERE Id = @Id",
            new { Id = id.ToString() });
    }

    public async Task CreateAsync(TaskItem task)
    {
        using var connection = CreateConnection();

        await connection.ExecuteAsync("""
            INSERT INTO Tasks 
            (Id, Title, Description, Status, Priority, DueDate, CreatedAt, UpdatedAt)
            VALUES 
            (@Id, @Title, @Description, @Status, @Priority, @DueDate, @CreatedAt, @UpdatedAt)
        """, new
        {
            Id = task.Id.ToString(),
            task.Title,
            task.Description,
            task.Status,
            task.Priority,
            task.DueDate,
            task.CreatedAt,
            task.UpdatedAt
        });
    }

    public async Task UpdateAsync(TaskItem task)
    {
        using var connection = CreateConnection();

        await connection.ExecuteAsync("""
            UPDATE Tasks
            SET Title = @Title,
                Description = @Description,
                Status = @Status,
                Priority = @Priority,
                DueDate = @DueDate,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id
        """, new
        {
            Id = task.Id.ToString(),
            task.Title,
            task.Description,
            task.Status,
            task.Priority,
            task.DueDate,
            task.UpdatedAt
        });
    }

    public async Task DeleteAsync(Guid id)
    {
        using var connection = CreateConnection();

        await connection.ExecuteAsync(
            "DELETE FROM Tasks WHERE Id = @Id",
            new { Id = id.ToString() });
    }
}