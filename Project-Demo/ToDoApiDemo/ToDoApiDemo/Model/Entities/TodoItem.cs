namespace ToDoApiDemo.Model.Entities;
public class TodoItem
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string Assignee { get; set; } = string.Empty;
    public int Priority { get; set; }
    public bool IsComplete { get; set; }
}
