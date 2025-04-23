using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoApi.Models.DTOs.ToDoLists;
using TodoApi.Services;
using TodoApi.Helpers;

namespace TodoApi.Controllers;

[Authorize(Roles = "Owner")]
[ApiController]
[Route("api/[controller]")]
public class ToDoListController : ControllerBase
{
    private readonly ToDoListService _toDoListService;

    public ToDoListController(ToDoListService toDoListService)
    {
        _toDoListService = toDoListService;
    }

    [HttpGet]
    public async Task<IActionResult> GetToDoLists(
        [FromQuery] Guid? categoryId,
        [FromQuery] string? searchText,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _toDoListService.GetAllToDoListsAsync(categoryId, searchText, page, pageSize);

        if (result == null)
        {
            return ApiResponser.NotFound("No to-do lists found.");
        }

        return ApiResponser.Success(result, "To-do lists retrieved successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetToDoListById(Guid id)
    {
        var list = await _toDoListService.GetToDoListByIdAsync(id);
        if (list == null)
        {
            return ApiResponser.NotFound("To-do list not found.");
        }

        return ApiResponser.Success(list, "To-do list retrieved successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateToDoList([FromBody] ToDoListCreateDto dto)
    {
        if (dto == null)
        {
            return ApiResponser.BadRequest("To-do list data is required.");
        }

        var createdList = await _toDoListService.CreateToDoListAsync(dto);
        return ApiResponser.Success(createdList, "To-do list created successfully.", (int)System.Net.HttpStatusCode.Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateToDoList(Guid id, [FromBody] ToDoListUpdateDto dto)
    {
        if (dto == null)
        {
            return ApiResponser.BadRequest("To-do list data is required.");
        }

        var updatedList = await _toDoListService.UpdateToDoListAsync(id, dto);
        if (updatedList == null)
        {
            return ApiResponser.NotFound("To-do list not found.");
        }

        return ApiResponser.Success(updatedList, "To-do list updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToDoList(Guid id)
    {
        var deleted = await _toDoListService.DeleteToDoListAsync(id);
        if (!deleted)
        {
            return ApiResponser.NotFound("To-do list not found.");
        }

        return ApiResponser.Success(null, "To-do list deleted successfully.", (int)System.Net.HttpStatusCode.NoContent);
    }
}
