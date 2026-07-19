using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Dtos;
using TaskTracker.Api.Models;
using TaskTracker.Api.Repositories;

namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _repository;

    public TasksController(ITaskRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _repository.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task is null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskRequest request)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = "Todo",
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(task);

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTaskRequest request)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing is null)
            return NotFound();

        existing.Title = request.Title;
        existing.Description = request.Description;
        existing.Status = request.Status;
        existing.Priority = request.Priority;
        existing.DueDate = request.DueDate;
        existing.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existing);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing is null)
            return NotFound();

        await _repository.DeleteAsync(id);

        return NoContent();
    }
}