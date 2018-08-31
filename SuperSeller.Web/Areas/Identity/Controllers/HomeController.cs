using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SuperSeller.Web.Areas.Identity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Ads()
        {
            return View();
        }
    }
}