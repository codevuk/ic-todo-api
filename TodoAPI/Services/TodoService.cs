using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoAPI.Services;

public class TodoService : ITodoService
{
    private readonly TodoDBContext _context;
    private readonly IMapper _mapper;
    
    public TodoService(TodoDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<TodoViewModel>> GetTodos()
    {
        var todos = await _context.Todos.ToListAsync();

        return _mapper.Map<List<TodoViewModel>>(todos);
    }

    public async Task<IEnumerable<TodoViewModel>> GetTodos(int userId)
    {
        var todos = await _context
            .Todos
            .Where(record => record.UserId == userId)
            .ToListAsync();

        return _mapper.Map<List<TodoViewModel>>(todos);
    }

    public async Task<TodoViewModel?> GetTodo(int id)
    {
        var todo = await _context
            .Todos
            .FirstOrDefaultAsync(record => record.Id == id);

        return todo == null ? null : _mapper.Map<TodoViewModel>(todo);
    }

    public async Task<TodoViewModel?> GetTodo(int id, int userId)
    {
        var todo = await _context
            .Todos
            .FirstOrDefaultAsync(record => record.Id == id);

        if (todo?.UserId != userId)
        {
            return null;
        }

        return _mapper.Map<TodoViewModel>(todo);
    }

    public async Task<TodoViewModel> CreateTodo(CreateTodoRequest request, int userId)
    {
        var todo = new Todo
        {
            UserId = userId,
            Title = request.Title,
            DueDate = request.DueDate,
            IsCompleted = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return _mapper.Map<TodoViewModel>(todo);
    }

    public async Task<TodoViewModel> UpdateTodo(UpdateTodoRequest request, int id, int userId)
    {
        var todo = await _context
            .Todos
            .FirstOrDefaultAsync(record => record.Id == id && record.UserId == userId);

        if (todo == null)
        {
            // Ideally you want have custom exception over here.
            throw new Exception("This todo does not exist");
        }

        todo.Title = request.Title;
        todo.DueDate = request.DueDate;
        todo.IsCompleted = request.IsCompleted;
        todo.UpdatedAt = DateTime.Now;

        _context.Update(todo);
        await _context.SaveChangesAsync();

        return _mapper.Map<TodoViewModel>(todo);
    }

    public async Task DeleteTodo(int id, int userId)
    {
        var todo = await _context
            .Todos
            .FirstOrDefaultAsync(record => record.Id == id && record.UserId == userId);
        
        if (todo == null)
        {
            // Ideally you want have custom exception over here.
            throw new Exception("This todo does not exist");
        }
        
        _context.Remove(todo);
        await _context.SaveChangesAsync();
    }
}