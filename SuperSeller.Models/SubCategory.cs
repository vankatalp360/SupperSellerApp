using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperSeller.Models
{
    public class SubCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}