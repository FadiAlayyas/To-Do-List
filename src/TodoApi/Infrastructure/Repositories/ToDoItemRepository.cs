using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Infrastructure.Interfaces;
using TodoApi.Models.DTOs.ToDoItems;
using TodoApi.Models.Entities;

namespace TodoApi.Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ToDoItemRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ToDoItemDto>> GetFilteredAsync(Guid? priorityId, Guid? categoryId, bool? isCompleted, string? searchText, int page = 1, int pageSize = 10)
        {
            var query = _context.ToDoItems
                .Include(t => t.ToDoList)
                .Include(t => t.Priority)
                .Include(t => t.Category)
                .AsQueryable();

            // Apply filters
            if (priorityId.HasValue)
                query = query.Where(t => t.PriorityId == priorityId.Value);

            if (categoryId.HasValue)
                query = query.Where(t => t.CategoryId == categoryId.Value);

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(t => t.Title.Contains(searchText) || t.Description.Contains(searchText));

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .OrderBy(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mappedItems = _mapper.Map<List<ToDoItemDto>>(items);

            return new PaginatedList<ToDoItemDto>(mappedItems, page, totalPages, totalItems);
        }


        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return await _context.ToDoItems
                .Include(t => t.ToDoList)
                .Include(t => t.Priority)
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<ToDoItem?> GetByIdAsync(Guid id)
        {
            return await _context.ToDoItems
                .Include(t => t.ToDoList)
                .Include(t => t.Priority)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<ToDoItem> AddAsync(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();

            var addedItem = await _context.ToDoItems
                .Include(t => t.Priority)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == item.Id);

            if (addedItem == null)
                throw new InvalidOperationException("Added item could not be found.");

            return addedItem;
        }

        public async Task<ToDoItem> UpdateAsync(ToDoItem item)
        {
            _context.ToDoItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            if (item != null)
            {
                _context.ToDoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
