using Microsoft.EntityFrameworkCore;
using TodoListApiRecreate_1.Data;
using TodoListApiRecreate_1.Dto;
using TodoListApiRecreate_1.Models;

namespace TodoListApiRecreate_1.Services;


//when fixing something and you know its right
//give it a second so compiler notices also.
public sealed class TodoService
{
    private readonly AppDbContext _db;

    public TodoService(AppDbContext db)
    {
        _db = db;
    }


    public async Task<IReadOnlyList<TodoResponse>> GetAllAsync()
    {
        return await _db.Todos
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Select(x => new TodoResponse
            {
                Id = x.Id,
                Title = x.Title,
                IsCompleted = x.IsCompleted,
                CreatedAtUtc = x.CreatedAtUtc
            })
            .ToListAsync();
    }

    public async Task<TodoResponse?> GetByIdAsync(int id)
    {
        TodoItem? todo = await _db.Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        return todo is null ? null : Map(todo); //Readme for explanation on error i had here.
    }

    public async Task<TodoResponse> CreateAsync(CreateTodoRequest request)
    {
        TodoItem todo = new(request.Title.Trim());

        _db.Todos.Add(todo);
        await _db.SaveChangesAsync();

        return Map(todo);
    }

    public async Task<TodoResponse> UpdateAsync(
        int id, UpdateTodoRequest request)
    {
        TodoItem? todo = await _db.Todos.FindAsync(id);

        if (todo is null)
            return null;

        todo.Update(request.Title.Trim(), request.IsCompleted);
        await _db.SaveChangesAsync();

        return Map(todo);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        TodoItem? todo = await _db.Todos.FindAsync(id);

        if (todo is null)
            return false;

        _db.Todos.Remove(todo);
        await _db.SaveChangesAsync();

        return true;
    }

    private static TodoResponse Map(TodoItem todo) => new()
    {
        Id = todo.Id,
        Title = todo.Title,
        IsCompleted = todo.IsCompleted,
        CreatedAtUtc = todo.CreatedAtUtc
    };




}
