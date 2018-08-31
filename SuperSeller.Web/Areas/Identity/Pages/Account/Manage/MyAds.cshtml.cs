using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperSeller.Common.Admin.ViewModels;
using SuperSeller.Models;
using SuperSeller.Services.Admin.Interfaces;
using SuperSeller.Services.Users.Interfaces;

namespace SuperSeller.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class MyAdsModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUsersService  usersService;
        private readonly IUserService userService;

        public MyAdsModel(UserManager<User> userManager, SignInManager<User> signInManager, IUsersService usersService, IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.usersService = usersService;
            this.userService = userService;
        }

        

        [TempData]
        public string StatusMessage { get; set; }

        public ICollection<AdsConciseViewModel> Ads { get; set; }


        public IActionResult OnGet()
        {
            var user = userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var userModel = usersService.GetUser(user.Id).Result;
            Ads = userModel.Ads;
            
            return Page();
        }
    }
}