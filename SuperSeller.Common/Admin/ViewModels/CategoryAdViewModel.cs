using System.Collections.Generic;

namespace SuperSeller.Common.Admin.ViewModels
{
    public class CategoryAdViewModel
    {
        public string Name { get; set; }
        public ICollection<SubCategoryConciseViewModel> SubCategories { get; set; }
    }
}