using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SuperSeller.Models;

namespace SuperSeller.Common.User.BindingModels
{
    public class CreateAdBindingModel
    { 
        public string Title { get; set; }

        public string CreatorId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public int ConditionId { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string ContactNumber { get; set; }
        public bool PromoEnable { get; set; }
        public DateTime PromoEnd { get; set; }

        public IFormFile FirstPicturePath { get; set; }
        public IFormFile SecondPicturePath { get; set; }
        public IFormFile ThirdPicturePath { get; set; }
    }
}