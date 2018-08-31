using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperSeller.Common.Admin.BindingModels;
using SuperSeller.Services.Admin;
using SuperSeller.Services.Admin.Interfaces;

namespace SuperSeller.Web.Areas.Admin.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService categorySevice;

        [BindProperty]
        public CreateCategoryBindingModel Input { get; set; }

        public CreateModel(ICategoryService categorySevice)
        {
            this.categorySevice = categorySevice;
        }

        public void OnGet()
        {
            this.TempData["showError"] = "None";
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Category/Create");
            }

            var isCreated = this.categorySevice.CreateCategory(Input).Result;

            if (!isCreated)
            {
                this.TempData["showError"] = "Block";
                this.TempData["errorMessage"] = "This category already exist!";
            }

            return RedirectToPage("Index");
        }
        
    }
}