using System.ComponentModel.DataAnnotations;

namespace SuperSeller.Models
{
    public class Condition
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}