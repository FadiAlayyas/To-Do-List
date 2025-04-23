using TodoApi.Models.DTOs.ToDoLists;
using TodoApi.Models.Entities;

namespace TodoApi.Infrastructure.Interfaces;

public interface IToDoListRepository
{
    Task<PaginatedList<ToDoListDto>> GetAllAsync(
           Guid? categoryId,
           string? searchText,
           int page,
           int pageSize
    );

    Task<ToDoList?> GetByIdAsync(Guid id);
    Task<ToDoList> AddAsync(ToDoList toDoList);
    Task<ToDoList> UpdateAsync(ToDoList toDoList);
    Task DeleteAsync(Guid id);
}