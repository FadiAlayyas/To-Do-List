namespace TodoApi.Models.DTOs.ToDoItems
{
    public class ToDoItemUpdateDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }

        public Guid PriorityId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
