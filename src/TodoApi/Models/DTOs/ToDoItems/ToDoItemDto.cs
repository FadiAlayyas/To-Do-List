using TodoApi.Models.DTOs.Categories;
using TodoApi.Models.DTOs.Priorities;

namespace TodoApi.Models.DTOs.ToDoItems
{
    public class ToDoItemDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }

        public PriorityDto Priority { get; set; } = default!;

        public CategoryDto Category { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
