namespace TodoApp.Models;

public class 
    User
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<Todo> Todos { get; set; }
}