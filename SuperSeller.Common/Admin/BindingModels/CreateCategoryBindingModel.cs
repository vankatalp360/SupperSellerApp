using System.ComponentModel.DataAnnotations;

namespace SuperSeller.Common.Admin.BindingModels
{
    public class CreateCategoryBindingModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}