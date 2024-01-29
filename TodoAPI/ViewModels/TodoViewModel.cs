namespace TodoApp.ViewModels;

public class TodoViewModel
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public DateTime? DueDate { get; set; } = null;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    
}