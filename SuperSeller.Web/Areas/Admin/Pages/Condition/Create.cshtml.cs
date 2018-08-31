using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperSeller.Common.Admin.BindingModels;
using SuperSeller.Services.Admin.Interfaces;

namespace SuperSeller.Web.Areas.Admin.Pages.Condition
{
    public class CreateModel : PageModel
    {
        private IConditionService conditionService;

        public CreateModel(IConditionService conditionService)
        {
            this.conditionService = conditionService;
        }

        [BindProperty]
        public CreateConditionBindingModel Input { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Condition/Create");
            }

            this.conditionService.AddCondition(Input.Name);
            

            return RedirectToPage("/Condition/Create");
        }
    }
}