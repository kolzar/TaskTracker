using TaskTracker.Api.Models;

namespace TaskTracker.Api.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task CreateAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);
}