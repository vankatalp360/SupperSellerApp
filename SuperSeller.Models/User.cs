using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SuperSeller.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FullName { get; set; }

        public string ImagePath { get; set; }
        
        public string Region { get; set; }
        
        public string City { get; set; }
        
        public string Address { get; set; }

        public string PostalCode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RegisteredDate { get; set; }

        public ICollection<Ad> Ads { get; set; } =  new List<Ad>();
        public ICollection<UsesrObservedAds> ObservedAds { get; set; } = new List<UsesrObservedAds>();

    }
}