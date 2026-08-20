using System.ComponentModel.DataAnnotations;
namespace TodoListApiRecreate_1.Dto;

public sealed class UpdateTodoRequest
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

}
