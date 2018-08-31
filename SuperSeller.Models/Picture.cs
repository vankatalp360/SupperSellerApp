using System.ComponentModel.DataAnnotations;

namespace SuperSeller.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public int AdId { get; set; }
        public Ad Ad { get; set; }

        [Required]
        public string Path { get; set; }
    }
}