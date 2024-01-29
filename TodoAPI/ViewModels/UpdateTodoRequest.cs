namespace TodoApp.ViewModels;

public class UpdateTodoRequest : CreateTodoRequest
{
    public bool IsCompleted { get; set; }
}