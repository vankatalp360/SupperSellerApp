using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperSeller.Common.Admin.ViewModels;
using SuperSeller.Services.Admin.Interfaces;

namespace SuperSeller.Web.Areas.Admin.Pages.Category
{
    public class AllModel : PageModel
    {
        private readonly ICategoryService categoryService;

        public ICollection<CategoryConciseViewModel> Categories { get; set; }

        public AllModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public void OnGet()
        {
            Categories = categoryService.GetAllCategories().Result;
        }

        public IActionResult OnGetDelete(int id)
        {
            categoryService.Delete(id);

            return RedirectToPage("All");
        }
    }
}