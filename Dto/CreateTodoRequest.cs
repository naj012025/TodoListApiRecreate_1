using System.ComponentModel.DataAnnotations;
namespace TodoListApiRecreate_1.Dto;

public sealed class CreateTodoRequest
{
    //Learned the using on top makes these Attributes accsessable.
    [Required]
    [StringLength(200, MinimumLength = 1)]//Learned that this attribut makes it so Title lengt is 200 with a minimum of 1 char Required.
    public string Title { get; set; } = string.Empty;
}
