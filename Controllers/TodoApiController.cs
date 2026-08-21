using Microsoft.AspNetCore.Mvc;
using TodoListApiRecreate_1.Dto;
using TodoListApiRecreate_1.Services;
namespace TodoListApiRecreate_1.Controllers;

[ApiController]
[Route("api/todos")]
public sealed class TodoApiController : ControllerBase
{
    private readonly TodoService _service;

    public TodoApiController(TodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TodoResponse>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet]
    public async Task<ActionResult<TodoResponse>> GetById(int id)
    {
        TodoResponse? todo = await _service.GetByIdAsync(id);
        return todo is null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<TodoResponse>> Create(CreateTodoRequest request)
    {
        TodoResponse todo = await _service.CreateAsync(request);
        return CreatedAtAction(
            nameof(GetById),
            new { id = todo.Id },
            todo);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TodoResponse>> Update(int id, UpdateTodoRequest request)
    {
        TodoResponse? todo = await _service.UpdateAsync(id, request);
        return todo is null ? NotFound() : Ok(todo);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
