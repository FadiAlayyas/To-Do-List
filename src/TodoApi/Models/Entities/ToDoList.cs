using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models.Entities
{
    public class ToDoList
    {
        public Guid Id { get; set; }

        [Required, MaxLength(150), MinLength(3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }

}
