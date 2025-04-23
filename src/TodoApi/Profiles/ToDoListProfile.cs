using AutoMapper;
using TodoApi.Models.Entities;
using TodoApi.Models.DTOs.ToDoLists;
using TodoApi.Models.DTOs.ToDoItems;

namespace TodoApi.Profiles
{
    public class ToDoListProfile : Profile
    {
        public ToDoListProfile()
        {
            CreateMap<ToDoListCreateDto, ToDoList>();

            CreateMap<ToDoListUpdateDto, ToDoList>();

            CreateMap<ToDoListDto, ToDoList>();

            CreateMap<ToDoList, ToDoListDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.ToDoItems, opt => opt.MapFrom(src => src.ToDoItems));
        }
    }
}
