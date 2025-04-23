namespace TodoApi.Models.DTOs.ToDoLists;

public class ToDoListCreateDto
{
    public string Title { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public Guid CategoryId { get; set; }
}

