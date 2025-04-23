using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models.Entities
{
    public class Category
    {
        [Required]
        public Guid Id { get; set; }

        [Required, MaxLength(100), MinLength(2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public ICollection<ToDoList> ToDoLists { get; set; } = new List<ToDoList>();
    }
}
