using TodoApi.Models.DTOs.Categories;
using TodoApi.Models.DTOs.ToDoItems;

namespace TodoApi.Models.DTOs.ToDoLists;

public class ToDoListDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public CategoryDto Category { get; set; } = default!;
    public DateTime CreatedAt { get; set; }

    public ICollection<ToDoItemDto> ToDoItems { get; set; } = new List<ToDoItemDto>();
}

