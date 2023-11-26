using AutoMapper;
using iPractice.Application.Contract.Dtos;
using iPractice.Domain.Models;

namespace iPractice.Application.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ClientDto, Client>().ReverseMap();
            CreateMap<PsychologistDto, Psychologist>().ReverseMap();
            CreateMap<AvailabilityDto, Availability>().ReverseMap();
            CreateMap<TimeSlotDto, TimeSlot>().ReverseMap();
        }
    }
}
