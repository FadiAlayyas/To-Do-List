using TodoApi.Models.Entities;

namespace TodoApi.Infrastructure.Interfaces;

public interface IPriorityRepository
{
    Task<IEnumerable<Priority>> GetAllAsync();
    Task<Priority?> GetByIdAsync(Guid id);
    Task<Priority> AddAsync(Priority priority);
    Task<Priority> UpdateAsync(Priority priority);
    Task<bool> DeleteAsync(Guid id);
}

