namespace TodoListApiRecreate_1.Models
{
    public sealed class TodoItem
    {
        public int Id { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public bool IsCompleted { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }
    }
}
