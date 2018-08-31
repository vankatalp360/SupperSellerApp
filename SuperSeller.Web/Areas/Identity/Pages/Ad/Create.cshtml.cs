using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperSeller.Common.Admin.BindingModels;
using SuperSeller.Common.Admin.ViewModels;
using SuperSeller.Common.User.BindingModels;
using SuperSeller.Models;
using SuperSeller.Services.Users;
using SuperSeller.Services.Users.Interfaces;

namespace SuperSeller.Web.Areas.Identity.Pages.Ad
{
    public class CreateModel : PageModel
    {
        private readonly IAdService adService;
        private readonly UserManager<User> userManager;

        public CreateModel(IAdService adService, UserManager<User> userManager)
        {
            this.adService = adService;
            this.userManager = userManager;
        }

        public ICollection<CategoryAdViewModel> Categories { get; set; }

        public ICollection<Condition> Conditions { get; set; }

        public string CreatorId { get; set; }

        [BindProperty]
        public CreateAdBindingModel Input { get; set; } = new CreateAdBindingModel();

        public IActionResult OnGet()
        {
            var user = userManager.GetUserAsync(this.User).Result;
            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            Categories = adService.GetCattegories();
            Conditions = adService.GetConditions();

            var userId = userManager.GetUserId(this.User);
            CreatorId = userId;
            return null;
        }

        public async Task<IActionResult> OnPost()
        {
            Input.CategoryId = int.Parse(Request.Form["CategoryId"].ToString());
            Input.ConditionId = Int32.Parse(Request.Form["ConditionId"].ToString());
            var picturesPaths = await SavePictures(Input);
            await adService.CreateAd(Input, picturesPaths);

            return Redirect("/");
        }

        private static async Task<ICollection<string>> SavePictures(CreateAdBindingModel model)
        {
            var pictures = new List<string>();
            if (model.FirstPicturePath != null)
            {
                var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Profile", $"{model.Title}.{model.CreatorId}.First.{model.FirstPicturePath.FileName}");
                var fileStream = new FileStream(fullFilePath, FileMode.Create);
                await model.FirstPicturePath.CopyToAsync(fileStream);
                var path = $"/images/Profile/{model.Title}.{model.CreatorId}.First.{model.FirstPicturePath.FileName}";
                pictures.Add(path);
            }
            if (model.SecondPicturePath != null)
            {
                var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Profile", $"{model.Title}.{model.CreatorId}.Second.{model.SecondPicturePath.FileName}");
                var fileStream = new FileStream(fullFilePath, FileMode.Create);
                await model.SecondPicturePath.CopyToAsync(fileStream);
                var path = $"/images/Profile/{model.Title}.{model.CreatorId}.Second.{model.SecondPicturePath.FileName}";
                pictures.Add(path);
            }
            if (model.ThirdPicturePath != null)
            {
                var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Profile", $"{model.Title}.{model.CreatorId}.Third.{model.ThirdPicturePath.FileName}");
                var fileStream = new FileStream(fullFilePath, FileMode.Create);
                await model.ThirdPicturePath.CopyToAsync(fileStream);
                var path = $"/images/Profile/{model.Title}.{model.CreatorId}.Third.{model.ThirdPicturePath.FileName}";
                pictures.Add(path);
            }
            return pictures;
        }
    }
}