using Microsoft.AspNetCore.Mvc;
using TodoApi.Models.DTOs.Priorities;
using Microsoft.AspNetCore.Authorization;
using TodoApi.Services;
using TodoApi.Helpers;

namespace TodoApi.Controllers;

[Authorize(Roles = "Owner")]
[ApiController]
[Route("api/[controller]")]
public class PriorityController : ControllerBase
{
    private readonly PriorityService _priorityService;

    public PriorityController(PriorityService priorityService)
    {
        _priorityService = priorityService;
    }

    // Get all priorities
    [HttpGet]
    public async Task<IActionResult> GetPriorities()
    {
        var priorities = await _priorityService.GetAllPrioritiesAsync();

        if (priorities == null || !priorities.Any())
        {
            return ApiResponser.NotFound("No priorities found.");
        }

        return ApiResponser.Success(priorities, "Priorities retrieved successfully.");
    }

    // Get priority by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPriorityById(Guid id)
    {
        var priority = await _priorityService.GetPriorityByIdAsync(id);

        if (priority == null)
        {
            return ApiResponser.NotFound("Priority not found.");
        }

        return ApiResponser.Success(priority, "Priority retrieved successfully.");
    }

    // Create a new priority
    [HttpPost]
    public async Task<IActionResult> CreatePriority([FromBody] PriorityCreateDto priorityCreateDto)
    {
        if (priorityCreateDto == null)
        {
            return ApiResponser.BadRequest("Priority data is required.");
        }

        var createdPriority = await _priorityService.CreatePriorityAsync(priorityCreateDto);
        return ApiResponser.Success(createdPriority, "Priority created successfully.", (int)System.Net.HttpStatusCode.Created);
    }

    // Update an existing priority
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePriority(Guid id, [FromBody] PriorityUpdateDto priorityUpdateDto)
    {
        if (priorityUpdateDto == null)
        {
            return ApiResponser.BadRequest("Priority data is required.");
        }

        var updatedPriority = await _priorityService.UpdatePriorityAsync(id, priorityUpdateDto);

        if (updatedPriority == null)
        {
            return ApiResponser.NotFound("Priority not found.");
        }

        return ApiResponser.Success(updatedPriority, "Priority updated successfully.");
    }

    // Delete a priority
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePriority(Guid id)
    {
        var deleted = await _priorityService.DeletePriorityAsync(id);

        if (!deleted)
        {
            return ApiResponser.NotFound("Priority not found.");
        }

        return ApiResponser.Success(null, "Priority deleted successfully.", (int)System.Net.HttpStatusCode.NoContent);
    }
}

