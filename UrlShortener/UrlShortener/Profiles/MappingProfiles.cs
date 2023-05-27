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
        }
    }
}
