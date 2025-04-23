using AutoMapper;
using TodoApi.Infrastructure.Interfaces;
using TodoApi.Models.DTOs.ToDoItems;
using TodoApi.Models.Entities;

namespace TodoApi.Services
{
    public class ToDoItemService
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IMapper _mapper;

        public ToDoItemService(IToDoItemRepository toDoItemRepository, IMapper mapper)
        {
            _toDoItemRepository = toDoItemRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ToDoItemDto>> GetAllToDoItemsAsync(Guid? priorityId, Guid? categoryId, bool? isCompleted, string? searchText, int page = 1, int pageSize = 10)
        {
            var items = await _toDoItemRepository.GetFilteredAsync(priorityId, categoryId, isCompleted, searchText, page, pageSize);
            return items;
        }

        public async Task<ToDoItemDto?> GetToDoItemByIdAsync(Guid id)
        {
            var item = await _toDoItemRepository.GetByIdAsync(id);
            return item == null ? null : _mapper.Map<ToDoItemDto>(item);
        }

        public async Task<ToDoItemDto> CreateToDoItemAsync(ToDoItemCreateDto createDto)
        {
            var item = _mapper.Map<ToDoItem>(createDto);
            var created = await _toDoItemRepository.AddAsync(item);
            return _mapper.Map<ToDoItemDto>(created);
        }

        public async Task<ToDoItemDto?> UpdateToDoItemAsync(Guid id, ToDoItemUpdateDto updateDto)
        {
            var existing = await _toDoItemRepository.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(updateDto, existing);
            var updated = await _toDoItemRepository.UpdateAsync(existing);
            return _mapper.Map<ToDoItemDto>(updated);
        }

        public async Task<bool> DeleteToDoItemAsync(Guid id)
        {
            var existing = await _toDoItemRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _toDoItemRepository.DeleteAsync(id);
            return true;
        }

        public async Task<ToDoItemDto?> ChangeStatusAsync(Guid id, bool isCompleted)
        {
            var existing = await _toDoItemRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.IsCompleted = isCompleted;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _toDoItemRepository.UpdateAsync(existing);
            return _mapper.Map<ToDoItemDto>(updated);
        }
    }
}
