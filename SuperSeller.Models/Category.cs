using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperSeller.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}