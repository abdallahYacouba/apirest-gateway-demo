namespace ToDoApiDemo.Model.ModelWrapper;

public class TodoItemDto
{
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public string? Title { get; set; }
}
