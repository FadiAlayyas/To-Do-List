namespace TodoApi.Models.DTOs.ToDoLists;

public class ToDoListUpdateDto
{
    public string Title { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public Guid CategoryId { get; set; }
}

