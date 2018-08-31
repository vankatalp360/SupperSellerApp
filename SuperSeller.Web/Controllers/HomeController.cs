using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuperSeller.Services.Users.Interfaces;
using SuperSeller.Web.Models;

namespace SuperSeller.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdService adService;

        public HomeController(IAdService adService)
        {
            this.adService = adService;
        }

        public IActionResult Index()
        {
            var ads = adService.GetAds(null);

            return View(ads.ToArray());
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            if (search == null)
            {
                var ads = adService.GetAds(null);
                return View(ads.ToArray());
            }
            else
            {
                var ads = adService.GetAds(search);
                return View(ads.ToArray());
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
