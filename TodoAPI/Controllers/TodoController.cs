using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Services;
using TodoApp.ViewModels;

namespace TodoAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TodoController : BaseAuthController
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(IEnumerable<TodoViewModel>))]
    public async Task<IActionResult> GetTodos()
    {
        var userId = GetUserId();
        
        var todos = await _todoService.GetTodos(userId);

        return Ok(todos);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces(typeof(TodoViewModel))]
    public async Task<IActionResult> GetTodo(int id)
    {
        var userId = GetUserId();
        
        var todo = await _todoService.GetTodo(id, userId);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(TodoViewModel))]
    public async Task<IActionResult> CreateTodo([FromBody]CreateTodoRequest request)
    {
        var userId = GetUserId();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var todo = await _todoService.CreateTodo(request, userId);

        return Ok(todo);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(TodoViewModel))]
    public async Task<IActionResult> UpdateTodo(int id, [FromBody]UpdateTodoRequest request)
    {
        var userId = GetUserId();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var todo = await _todoService.UpdateTodo(request, id, userId);
            return Ok(todo);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(typeof(TodoViewModel))]
    public async Task<IActionResult> UpdateTodo(int id)
    {
        var userId = GetUserId();
        
        try
        {
            await _todoService.DeleteTodo(id, userId);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}