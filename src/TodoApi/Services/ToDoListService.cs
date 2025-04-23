using AutoMapper;
using TodoApi.Infrastructure.Interfaces;
using TodoApi.Models.DTOs.ToDoLists;
using TodoApi.Models.Entities;

namespace TodoApi.Services;

public class ToDoListService
{
    private readonly IToDoListRepository _toDoListRepository;
    private readonly IMapper _mapper;

    public ToDoListService(IToDoListRepository toDoListRepository, IMapper mapper)
    {
        _toDoListRepository = toDoListRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ToDoListDto>> GetAllToDoListsAsync(Guid? categoryId, string? searchText, int page = 1, int pageSize = 10)
    {
        var items = await _toDoListRepository.GetAllAsync(categoryId, searchText, page, pageSize);
        return items;
    }

    public async Task<ToDoListDto?> GetToDoListByIdAsync(Guid id)
    {
        var list = await _toDoListRepository.GetByIdAsync(id);
        return list == null ? null : _mapper.Map<ToDoListDto>(list);
    }

    public async Task<ToDoListDto> CreateToDoListAsync(ToDoListCreateDto createDto)
    {
        var list = _mapper.Map<ToDoList>(createDto);
        var created = await _toDoListRepository.AddAsync(list);
        return _mapper.Map<ToDoListDto>(created);
    }

    public async Task<ToDoListDto?> UpdateToDoListAsync(Guid id, ToDoListUpdateDto updateDto)
    {
        var existing = await _toDoListRepository.GetByIdAsync(id);
        if (existing == null) return null;

        _mapper.Map(updateDto, existing);
        var updated = await _toDoListRepository.UpdateAsync(existing);
        return _mapper.Map<ToDoListDto>(updated);
    }

    public async Task<bool> DeleteToDoListAsync(Guid id)
    {
        var existing = await _toDoListRepository.GetByIdAsync(id);
        if (existing == null) return false;

        await _toDoListRepository.DeleteAsync(id);
        return true;
    }
}

