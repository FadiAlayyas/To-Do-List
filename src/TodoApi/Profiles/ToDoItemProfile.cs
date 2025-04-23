using AutoMapper;
using TodoApi.Models.Entities;
using TodoApi.Models.DTOs.ToDoItems;

namespace TodoApi.Profiles
{
    public class ToDoItemProfile : Profile
    {
        public ToDoItemProfile()
        {
            CreateMap<ToDoItem, ToDoItemDto>().ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority)).ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<ToDoItemCreateDto, ToDoItem>();

            CreateMap<ToDoItemUpdateDto, ToDoItem>();

            CreateMap<ToDoItem, ToDoItemDto>();
        }
    }
}
