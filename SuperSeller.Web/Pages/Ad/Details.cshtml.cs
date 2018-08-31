using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperSeller.Common.User.ViewModels;
using SuperSeller.Services.Users.Interfaces;

namespace SuperSeller.Web.Pages.Ad
{
    public class DetailsModel : PageModel
    {
        private IAdService adService;

        public DetailsModel(IAdService adService)
        {
            this.adService = adService;
        }

        public AdsViewModel ViewModel { get; set; }

        public IActionResult OnGet(int id)
        {
            ViewModel = adService.GetAdDetails(id);
            if (ViewModel != null)
            {
                foreach (var picture in ViewModel.Pictures)
                {
                    ViewModel.PicturesPaths.Add(picture.Path);
                }
                return null;
            }

            return Redirect("/");
        }
    }
}