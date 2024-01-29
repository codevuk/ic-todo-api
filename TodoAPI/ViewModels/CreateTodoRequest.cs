using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels;

public class CreateTodoRequest
{
    [Required]
    [MinLength(2)]
    [MaxLength(256)]
    public string Title { get; set; }

    public DateTime? DueDate { get; set; } = null;
}