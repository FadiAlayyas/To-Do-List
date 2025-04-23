using TodoApi.Models.Entities;

using AutoMapper;
using TodoApi.Models.DTOs.Categories;
using TodoApi.Infrastructure.Interfaces;

namespace TodoApi.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        var createdCategory = await _categoryRepository.AddAsync(category);
        return _mapper.Map<CategoryDto>(createdCategory);
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(Guid id, CategoryUpdateDto categoryDto)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return null;
        }

        _mapper.Map(categoryDto, existingCategory);
        var updatedCategory = await _categoryRepository.UpdateAsync(existingCategory);
        return _mapper.Map<CategoryDto>(updatedCategory);
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return false;
        }
        await _categoryRepository.DeleteAsync(id);
        return true;
    }
}
