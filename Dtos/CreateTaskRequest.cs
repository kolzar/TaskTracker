namespace TaskTracker.Api.Dtos;

public class CreateTaskRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Priority { get; set; } = "Medium";
    public DateTime? DueDate { get; set; }
}