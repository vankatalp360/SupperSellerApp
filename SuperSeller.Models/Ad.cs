using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperSeller.Models
{
    public class Ad
    {
        public int Id { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }
        public SubCategory Category { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public int ConditionId { get; set; }
        public Condition Condition { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        public int Viewing { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string ContactNumber { get; set; }
        public bool PromoEnable { get; set; }
        public DateTime PromoEnd { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        
        public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
        public ICollection<UsesrObservedAds> Viewers { get; set; } = new List<UsesrObservedAds>();
    }
}