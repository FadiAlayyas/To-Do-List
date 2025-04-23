using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Infrastructure.Interfaces;
using TodoApi.Models.Entities;

namespace TodoApi.Infrastructure.Repositories;

public class PriorityRepository : IPriorityRepository
{
    private readonly ApplicationDbContext _context;

    public PriorityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Priority>> GetAllAsync()
    {
        return await _context.Priorities.ToListAsync();
    }

    public async Task<Priority?> GetByIdAsync(Guid id)
    {
        return await _context.Priorities.FindAsync(id);
    }

    public async Task<Priority> AddAsync(Priority priority)
    {
        await _context.Priorities.AddAsync(priority);
        await _context.SaveChangesAsync();
        return priority;
    }

    public async Task<Priority> UpdateAsync(Priority priority)
    {
        _context.Priorities.Update(priority);
        await _context.SaveChangesAsync();
        return priority;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var priority = await _context.Priorities.FindAsync(id);
        if (priority == null) return false;

        _context.Priorities.Remove(priority);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}

