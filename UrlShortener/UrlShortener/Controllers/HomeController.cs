using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Services;
using UrlShortener.ViewModels;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlDataRepository _urlDataRepository;
        private readonly IMapper _mapper;

        public HomeController(IUrlDataRepository urlDataRepository, IMapper mapper, IShortUrlService shortUrlService, UserManager<IdentityUser> userManager)
        {
            _urlDataRepository = urlDataRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var urlDatas = await _urlDataRepository.GetAllAsync();

            var mappedUrlData = _mapper.Map<IEnumerable<UrlViewModel>>(urlDatas);
            
            return View(mappedUrlData);
        }
    }

}