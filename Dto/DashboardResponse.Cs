namespace TodoListApiRecreate_1.Dto;

public sealed class DashboardResponse
{
    public AtmAccountResponse Accounts { get; init; } = new();
    public IReadOnlyList<TodoResponse> Todos { get; init; } = [];
}
