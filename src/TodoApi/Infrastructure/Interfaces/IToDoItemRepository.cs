using TodoApi.Models.DTOs.ToDoItems;
using TodoApi.Models.Entities;

namespace TodoApi.Infrastructure.Interfaces
{
    public interface IToDoItemRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllAsync();
        Task<ToDoItem?> GetByIdAsync(Guid id);
        Task<ToDoItem> AddAsync(ToDoItem item);
        Task<ToDoItem> UpdateAsync(ToDoItem item);
        Task DeleteAsync(Guid id);

        // Corrected: Return strongly typed pagination response
        Task<PaginatedList<ToDoItemDto>> GetFilteredAsync(
            Guid? priorityId,
            Guid? categoryId,
            bool? isCompleted,
            string? searchText,
            int page,
            int pageSize
        );
    }
}
