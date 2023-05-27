using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}