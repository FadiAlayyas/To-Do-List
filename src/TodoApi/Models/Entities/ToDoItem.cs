using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models.Entities
{
    public class ToDoItem
    {
        [Required]
        public Guid Id { get; set; }

        [Required, MaxLength(200), MinLength(3)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsCompleted { get; set; } = false;

        [Required]
        public DateTime DueDate { get; set; }

        // Foreign keys are nullable
        public Guid? ToDoListId { get; set; }
        public ToDoList ToDoList { get; set; }

        public Guid? PriorityId { get; set; }
        public Priority Priority { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
