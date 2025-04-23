using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Infrastructure.Interfaces;
using TodoApi.Models.DTOs.ToDoLists;
using TodoApi.Models.Entities;

namespace TodoApi.Infrastructure.Repositories;

public class ToDoListRepository : IToDoListRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ToDoListRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ToDoListDto>> GetAllAsync(
        Guid? categoryId = null,
        string? searchText = null,
        int page = 1,
        int pageSize = 10)
    {
        // Validate input parameters
        if (page < 1) page = 1;
        if (pageSize < 1 || pageSize > 100) pageSize = 10;

        var query = _context.ToDoLists
                .AsNoTracking()
                .Include(t => t.Category)
                .Include(t => t.User)
                .Include(t => t.ToDoItems)
                    .ThenInclude(i => i.Priority)
                .Include(t => t.ToDoItems)
                    .ThenInclude(i => i.Category)
                .AsQueryable();

        // Apply filters
        if (categoryId.HasValue)
        {
            query = query.Where(t => t.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(t => t.Title.Contains(searchText));
        }

        // Get total count before pagination
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        // Apply pagination
        var items = await query
            .OrderBy(t => t.Title) // Or any other suitable ordering
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var mappedItems = _mapper.Map<List<ToDoListDto>>(items);

        return new PaginatedList<ToDoListDto>(mappedItems, page, totalPages, totalItems);
    }

    public async Task<ToDoList?> GetByIdAsync(Guid id)
    {
        return await _context.ToDoLists
            .Include(t => t.Category)
            .Include(t => t.User)
            .Include(t => t.ToDoItems)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<ToDoList> AddAsync(ToDoList toDoList)
    {
        await _context.ToDoLists.AddAsync(toDoList);
        await _context.SaveChangesAsync();
        return toDoList;
    }

    public async Task<ToDoList> UpdateAsync(ToDoList toDoList)
    {
        _context.ToDoLists.Update(toDoList);
        await _context.SaveChangesAsync();
        return toDoList;
    }

    public async Task DeleteAsync(Guid id)
    {
        var list = await _context.ToDoLists.FindAsync(id);
        if (list != null)
        {
            _context.ToDoLists.Remove(list);
            await _context.SaveChangesAsync();
        }
    }
}

