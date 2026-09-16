using TodoListApiRecreate_1.Dto;
namespace TodoListApiRecreate_1.Services;

public sealed class DashboardService(
    TodoService todoService,
    AtmApiClient atmApiClient)
{
    public async Task<DashboardResponse> GetAsync(
        int accountId,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        IReadOnlyList<TodoResponse> todos =
            await todoService.GetAllAsync(accountId);

        AtmAccountResponse account =
            await atmApiClient.GetMyAccountAsync(
                accessToken, cancellationToken)
            ?? throw new InvalidOperationException(
                "AtmApi return no account response");
        return new DashboardResponse
        {
            Accounts = account,
            Todos = todos
        };
    }
}
