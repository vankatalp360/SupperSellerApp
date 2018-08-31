using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperSeller.Models;
using SuperSeller.Services.Users.Interfaces;

namespace SuperSeller.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AdsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IAdService adService;

        public AdsController(UserManager<User> userManager, IAdService adService)
        {
            this.userManager = userManager;
            this.adService = adService;
        }

        public IActionResult Delete(int id)
        {

            var user = userManager.GetUserAsync(this.User).Result;
            if (user == null)
            {
                this.ViewData["errorMessage"] = "You do not have permision to this ad!";
                return Redirect("/");
            }
            var isAdmin = userManager.IsInRoleAsync(user, "Administrator").Result;
            var isDelete = adService.DeleteAd(id, user, isAdmin);
            if (isDelete)
            {
                this.ViewData["successMessage"] = "Ad is successfully removed";
                return Redirect("/");
            }

            this.ViewData["errorMessage"] = "You do not have permision to this ad!";
            return Redirect("/");
        }
    }
}