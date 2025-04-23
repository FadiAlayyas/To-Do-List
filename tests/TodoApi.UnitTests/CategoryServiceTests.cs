using Xunit;
using Moq;
using AutoMapper;
using TodoApi.Services;
using TodoApi.Models.DTOs.Categories;
using TodoApi.Models.Entities;
using TodoApi.Infrastructure.Interfaces;
using FluentAssertions;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CategoryService _service;

    public CategoryServiceTests()
    {
        _mockRepo = new Mock<ICategoryRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new CategoryService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllCategoriesAsync_ShouldReturnDtos()
    {
        // Arrange
        var fakeCategories = new List<Category> { new Category { Id = Guid.NewGuid(), Name = "Test" } };
        var mappedDtos = new List<CategoryDto> { new CategoryDto { Name = "Test" } };

        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(fakeCategories);
        _mockMapper.Setup(m => m.Map<IEnumerable<CategoryDto>>(fakeCategories)).Returns(mappedDtos);

        // Act
        var result = await _service.GetAllCategoriesAsync();

        // View in console
        foreach (var category in result)
        {
            Console.WriteLine($"Category Name: {category.Name}");
        }

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Test");
    }
}
