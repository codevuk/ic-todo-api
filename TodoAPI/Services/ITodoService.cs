using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoAPI.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoViewModel>> GetTodos();
    
    Task<IEnumerable<TodoViewModel>> GetTodos(int userId);

    Task<TodoViewModel?> GetTodo(int id);
    
    Task<TodoViewModel?> GetTodo(int id, int userId);

    Task<TodoViewModel> CreateTodo(CreateTodoRequest request, int userId);
    
    Task<TodoViewModel> UpdateTodo(UpdateTodoRequest request, int id, int userId);

    Task DeleteTodo(int id, int userId);
}