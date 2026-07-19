namespace TaskTracker.Api.Models;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "Todo";
    public string Priority { get; set; } = "Medium";
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}