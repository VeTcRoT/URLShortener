using AutoMapper;
using UrlShortener.Models;
using UrlShortener.ViewModels;

namespace UrlShortener.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UrlData, UrlViewModel>();

            CreateMap<UrlData, UrlDetailsViewModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
