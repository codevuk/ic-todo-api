using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels;

public class RegisterRequest
{
    [Required]
    [MinLength(3)]
    [MaxLength(32)]
    public string Username { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}