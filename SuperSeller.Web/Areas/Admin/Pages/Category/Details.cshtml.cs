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
    public class DetailsModel : PageModel
    {
        private readonly ICategoryService categoryService;

        public CategoryViewModel Category { get; set; }

        public DetailsModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public void OnGet(int id)
        {
            Category = categoryService.GetCategory(id).Result;
        }
    }
}