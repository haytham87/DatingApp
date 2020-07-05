using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
             src.Photo.FirstOrDefault(p=>p.IsMain).Url))
             .ForMember(dest => dest.Age,opt => opt.MapFrom(d=> d.DateofBith.CalculateAge()));

            CreateMap<User, UserForDetailDto>()
            .ForMember(dest => dest.photo, opt => opt.MapFrom(src =>
             src.Photo.FirstOrDefault(p=>p.IsMain).Url))
             .ForMember(dest => dest.Age,opt => opt.MapFrom(d=> d.DateofBith.CalculateAge()));
             
            CreateMap<Photo,PhotoForDetailedDto>();
        }
    }
}