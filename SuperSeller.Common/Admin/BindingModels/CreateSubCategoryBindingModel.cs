using System.ComponentModel.DataAnnotations;

namespace SuperSeller.Common.Admin.BindingModels
{
    public class CreateSubCategoryBindingModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}