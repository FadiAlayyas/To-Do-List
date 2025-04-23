using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoApi.Models.DTOs.ToDoItems;
using TodoApi.Services;
using TodoApi.Helpers;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemController : ControllerBase
{
    private readonly ToDoItemService _toDoItemService;

    public ToDoItemController(ToDoItemService toDoItemService)
    {
        _toDoItemService = toDoItemService;
    }

    // Get all to-do items with optional filters
    [Authorize(Roles = "Guest,owner")]
    [HttpGet]
    public async Task<IActionResult> GetToDoItems(
    [FromQuery] Guid? priorityId,
    [FromQuery] Guid? categoryId,
    [FromQuery] bool? isCompleted,
    [FromQuery] string? searchText,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var result = await _toDoItemService.GetAllToDoItemsAsync(
            priorityId, categoryId, isCompleted, searchText, page, pageSize);

        if (result == null)
        {
            return ApiResponser.NotFound("No to-do items found.");
        }

        return ApiResponser.Success(result, "To-do items retrieved successfully.");
    }

    // Get to-do item by ID
    [Authorize(Roles = "Guest,owner")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetToDoItemById(Guid id)
    {
        var item = await _toDoItemService.GetToDoItemByIdAsync(id);
        if (item == null)
        {
            return ApiResponser.NotFound("To-do item not found.");
        }

        return ApiResponser.Success(item, "To-do item retrieved successfully.");
    }

    // Create a new to-do item
    [Authorize(Roles = "owner")]
    [HttpPost]
    public async Task<IActionResult> CreateToDoItem([FromBody] ToDoItemCreateDto dto)
    {
        if (dto == null)
        {
            return ApiResponser.BadRequest("To-do item data is required.");
        }

        var createdItem = await _toDoItemService.CreateToDoItemAsync(dto);
        return ApiResponser.Success(createdItem, "To-do item created successfully.", (int)System.Net.HttpStatusCode.Created);
    }

    // Update an existing to-do item
    [Authorize(Roles = "owner")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateToDoItem(Guid id, [FromBody] ToDoItemUpdateDto dto)
    {
        if (dto == null)
        {
            return ApiResponser.BadRequest("To-do item data is required.");
        }

        var updatedItem = await _toDoItemService.UpdateToDoItemAsync(id, dto);
        if (updatedItem == null)
        {
            return ApiResponser.NotFound("To-do item not found.");
        }

        return ApiResponser.Success(updatedItem, "To-do item updated successfully.");
    }

    // Delete a to-do item
    [Authorize(Roles = "owner")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToDoItem(Guid id)
    {
        var deleted = await _toDoItemService.DeleteToDoItemAsync(id);
        if (!deleted)
        {
            return ApiResponser.NotFound("To-do item not found.");
        }

        return ApiResponser.Success(null, "To-do item deleted successfully.", (int)System.Net.HttpStatusCode.NoContent);
    }

    // Change status of a to-do item
    [Authorize(Roles = "Guest,owner")]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromQuery] bool isCompleted)
    {
        var updated = await _toDoItemService.ChangeStatusAsync(id, isCompleted);
        if (updated == null)
        {
            return ApiResponser.NotFound("To-do item not found.");
        }

        return ApiResponser.Success(updated, "To-do item status updated successfully.");
    }
}
