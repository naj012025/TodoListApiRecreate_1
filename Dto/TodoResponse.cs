namespace TodoListApiRecreate_1.Dto;

public sealed class TodoResponse
{

    // init instead of set on outgoing info to the user is initialized and cant be changed.
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public bool IsCompleted { get; init; }
    public DateTime CreatedAtUtc { get; init; }

}
