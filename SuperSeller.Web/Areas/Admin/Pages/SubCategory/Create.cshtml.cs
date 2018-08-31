using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperSeller.Common.Admin.BindingModels;
using SuperSeller.Common.Admin.ViewModels;
using SuperSeller.Services.Admin.Interfaces;

namespace SuperSeller.Web.Areas.Admin.Pages.SubCategory
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService categorySevice;

        [BindProperty]
        public CreateSubCategoryBindingModel Input { get; set; }

        public IEnumerable<SelectCategoryViewModel> Categories { get; set; }

        public CreateModel(ICategoryService categorySevice)
        {
            this.categorySevice = categorySevice;
        }

        public void OnGet(int? id)
        {
            this.TempData["showError"] = "None";
            if (id != null)
            {
                TempData["Selected"] = id;
            }
            Categories = categorySevice.GetAllSelectedCategories().Result;
        }

        public IActionResult OnPost()
        {
            Input.CategoryId = int.Parse(Request.Form["CategoryId"].ToString());
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/SubCategory/Create");
            }

            var isCreated = this.categorySevice.CreateSubCategory(Input).Result;

            if (!isCreated)
            {
                this.TempData["showError"] = "Block";
                this.TempData["errorMessage"] = "This sub category already exist!";
            }

            return RedirectToPage("Index");
        }
    }
}