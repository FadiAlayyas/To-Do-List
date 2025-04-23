using AutoMapper;
using TodoApi.Models.Entities;
using TodoApi.Models.DTOs.Categories;

namespace TodoApi.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
