using System.Collections.Generic;

namespace SuperSeller.Common.Admin.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<SubCategoryConciseViewModel> SubCategories { get; set; }
    }
}