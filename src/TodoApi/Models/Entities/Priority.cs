using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models.Entities
{
    public class Priority
    {
        public Guid Id { get; set; }

        [Required, MaxLength(50), MinLength(2)]
        public string Name { get; set; } = string.Empty;

        public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }
}