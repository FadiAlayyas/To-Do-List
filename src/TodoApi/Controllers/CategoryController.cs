using Microsoft.AspNetCore.Mvc;
using TodoApi.Models.DTOs.Categories;
using Microsoft.AspNetCore.Authorization;
using TodoApi.Services;
using TodoApi.Helpers;

namespace TodoApi.Controllers;

[Authorize(Roles = "Owner")]
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // Get all categories
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        if (categories == null || !categories.Any())
        {
            return ApiResponser.NotFound("No categories found.");
        }

        return ApiResponser.Success(categories, "Categories retrieved successfully.");
    }

    // Get category by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return ApiResponser.NotFound("Category not found.");
        }

        return ApiResponser.Success(category, "Category retrieved successfully.");
    }

    // Create new category
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryDto)
    {
        if (categoryDto == null)
        {
            return ApiResponser.BadRequest("Category data is required.");
        }

        var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
        return ApiResponser.Success(createdCategory, "Category created successfully.", (int)System.Net.HttpStatusCode.Created);
    }

    // Update an existing category
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryDto)
    {
        if (categoryDto == null)
        {
            return ApiResponser.BadRequest("Category data is required.");
        }

        var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);

        if (updatedCategory == null)
        {
            return ApiResponser.NotFound("Category not found.");
        }

        return ApiResponser.Success(updatedCategory, "Category updated successfully.");
    }

    // Delete a category
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var deleted = await _categoryService.DeleteCategoryAsync(id);

        if (!deleted)
        {
            return ApiResponser.NotFound("Category not found.");
        }

        return ApiResponser.Success(null, "Category deleted successfully", (int)System.Net.HttpStatusCode.NoContent);
    }
}

