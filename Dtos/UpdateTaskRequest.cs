namespace TaskTracker.Api.Dtos;

public class UpdateTaskRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "Todo";
    public string Priority { get; set; } = "Medium";
    public DateTime? DueDate { get; set; }
}