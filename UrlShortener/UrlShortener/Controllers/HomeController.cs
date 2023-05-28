using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Text.RegularExpressions;
using UrlShortener.Models;
using UrlShortener.Services;
using UrlShortener.ViewModels;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlDataRepository _urlDataRepository;
        private readonly IMapper _mapper;
        private readonly IShortUrlService _shortUrlService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IUrlDataRepository urlDataRepository, 
            IMapper mapper, 
            IShortUrlService shortUrlService, 
            UserManager<IdentityUser> userManager)
        {
            _urlDataRepository = urlDataRepository;
            _mapper = mapper;
            _shortUrlService = shortUrlService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var urlDatas = await _urlDataRepository.GetAllAsync();

            var mappedUrlData = _mapper.Map<IEnumerable<UrlViewModel>>(urlDatas);
            
            return View(mappedUrlData);
        }

        public async Task<IActionResult> UserUrls()
        {
            var user = await _userManager.GetUserAsync(User);

            var userUrls = await _urlDataRepository.GetUserUrls(user);

            var mappedUrls = _mapper.Map<IEnumerable<UrlViewModel>>(userUrls);

            return View(mappedUrls);
        }

        public async Task<IActionResult> AddUrl(string url)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                return RedirectToAction("Index");
            }

            var response = await _shortUrlService.CheckUrlAsync(url);
            
            if (!response)
            {
                return RedirectToAction("Index");
            }

            var urlData = await _urlDataRepository.GetByOriginalUrl(url);

            if (urlData != null)
            {
                return RedirectToAction("Index");
            }

            string shortedUrl = _shortUrlService.GenerateShortUrl(url);

            var user = await _userManager.GetUserAsync(User);

            var newUrlData = new UrlData()
            {
                OriginalUrl = url,
                ShortUrl = shortedUrl,
                CreatedDate = DateTime.Now,
                User = user
            };

            await _urlDataRepository.AddAsync(newUrlData);

            return RedirectToAction("Index");
        }
    }

}