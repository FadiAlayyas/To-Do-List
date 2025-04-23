using TodoApi.Models.Entities;

using AutoMapper;
using TodoApi.Models.DTOs.Priorities;
using TodoApi.Infrastructure.Interfaces;

namespace TodoApi.Services;

public class PriorityService
{
    private readonly IPriorityRepository _priorityRepository;
    private readonly IMapper _mapper;

    public PriorityService(IPriorityRepository priorityRepository, IMapper mapper)
    {
        _priorityRepository = priorityRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
    {
        var priorities = await _priorityRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PriorityDto>>(priorities);
    }

    public async Task<PriorityDto> GetPriorityByIdAsync(Guid id)
    {
        var priority = await _priorityRepository.GetByIdAsync(id);
        return _mapper.Map<PriorityDto>(priority);
    }

    public async Task<PriorityDto> CreatePriorityAsync(PriorityCreateDto priorityDto)
    {
        var priority = _mapper.Map<Priority>(priorityDto);
        var createdPriority = await _priorityRepository.AddAsync(priority);
        return _mapper.Map<PriorityDto>(createdPriority);
    }

    public async Task<PriorityDto?> UpdatePriorityAsync(Guid id, PriorityUpdateDto priorityDto)
    {
        var existingPriority = await _priorityRepository.GetByIdAsync(id);
        if (existingPriority == null)
        {
            return null;
        }

        _mapper.Map(priorityDto, existingPriority);
        var updatedPriority = await _priorityRepository.UpdateAsync(existingPriority);
        return _mapper.Map<PriorityDto>(updatedPriority);
    }

    public async Task<bool> DeletePriorityAsync(Guid id)
    {
        var priority = await _priorityRepository.GetByIdAsync(id);
        if (priority == null)
        {
            return false;
        }
        await _priorityRepository.DeleteAsync(id);
        return true;
    }
}
