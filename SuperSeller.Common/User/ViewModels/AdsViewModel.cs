using System;
using System.Collections.Generic;
using SuperSeller.Models;

namespace SuperSeller.Common.User.ViewModels
{
    public class AdsViewModel
    {
        public int Id { get; set; }

        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string CreatorPicture { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }
        public SubCategory Category { get; set; }
        
        public decimal Price { get; set; }

        public string Condition { get; set; }
        
        public string Description { get; set; }
        
        public string Region { get; set; }
        
        public string City { get; set; }
        
        public string PostalCode { get; set; }

        public int Viewing { get; set; }
        
        public string Email { get; set; }
        
        public string ContactNumber { get; set; }
        public bool PromoEnable { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<Picture> Pictures { get; set; }
        public ICollection<string> PicturesPaths { get; set; } = new List<string>();
    }
}