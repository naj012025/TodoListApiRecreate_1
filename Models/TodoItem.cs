namespace TodoListApiRecreate_1.Models;

public sealed class TodoItem
{
    public int Id { get; private set; }

    public int AccountId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public bool IsCompleted { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    private TodoItem()
    {
        //empty atm ef core can use this when materialisng rows.
    }

    public TodoItem(int accountId, string title)
    {
        AccountId = accountId;
        Title = title;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Update(string title, bool isCompleted)
    {
        Title = title;
        IsCompleted = isCompleted;
    }

}
