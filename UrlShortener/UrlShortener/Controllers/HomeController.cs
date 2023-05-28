﻿using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShortener.Models;
using UrlShortener.Services;
using UrlShortener.ViewModels;

namespace UrlShortener.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var urlDatas = await _urlDataRepository.GetAllAsync();

            var mappedUrlData = _mapper.Map<IEnumerable<UrlViewModel>>(urlDatas);

            var paginationData = Pagination(mappedUrlData, page);
            
            return View(paginationData);
        }

        public async Task<IActionResult> UserUrls(int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);

            var userUrls = await _urlDataRepository.GetUserUrls(user);

            var mappedUrls = _mapper.Map<IEnumerable<UrlViewModel>>(userUrls);

            var paginationData = Pagination(mappedUrls, page);

            return View(paginationData);
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

        public async Task<IActionResult> Details(int id)
        {
            var urlData = await _urlDataRepository.GetByIdAsync(id);

            if (urlData == null) 
            { 
                return RedirectToAction("Index");
            }

            var mappedUrlData = _mapper.Map<UrlDetailsViewModel>(urlData);

            return View(mappedUrlData);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var urlData = await _urlDataRepository.GetByIdAsync(id);

            if (urlData == null)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (User.IsInRole(Roles.Admin.ToString()) || currentUser.Id == urlData.User.Id)
            {
                await _urlDataRepository.DeleteAsync(urlData);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> RedirectToShortedUrl(string url)
        {
            var splitedUrl = url.Split('/');

            var shortedUrl = splitedUrl[splitedUrl.Length - 1];

            var urlData = await _urlDataRepository.GetByShortUrl(shortedUrl);

            if (urlData == null)
                return NotFound();

            return Redirect(urlData.OriginalUrl);
        }

        private IEnumerable<UrlViewModel> Pagination(IEnumerable<UrlViewModel> data, int page)
        {
            const int pageSize = 10;

            if (page < 1)
            {
                page = 1;
            }

            int recsCount = data.Count();

            var pager = new Pager(recsCount, page, pageSize);

            int recSkip = (page - 1) * pageSize;

            data = data.Skip(recSkip).Take(pager.PageSize).ToList();

            ViewBag.Pager = pager;

            return data;
        }
    }

}