using AutoMapper;
using TodoApi.Models.Entities;
using TodoApi.Models.DTOs.Priorities;

namespace TodoApi.Profiles
{
    public class PriorityProfile : Profile
    {
        public PriorityProfile()
        {
            CreateMap<Priority, PriorityDto>();
            
            CreateMap<PriorityCreateDto, Priority>();

            CreateMap<PriorityUpdateDto, Priority>();

            CreateMap<PriorityDto, Priority>();
        }
    }
}
