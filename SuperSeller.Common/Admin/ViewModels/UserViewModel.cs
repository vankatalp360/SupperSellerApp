using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SuperSeller.Models;

namespace SuperSeller.Common.Admin.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegisteredDate { get; set; }

        public int ObservedAds { get; set; }

        public ICollection<AdsConciseViewModel> Ads { get; set; }
    }
}