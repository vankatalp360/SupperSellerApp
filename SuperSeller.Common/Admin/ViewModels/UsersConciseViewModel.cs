using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SuperSeller.Models;

namespace SuperSeller.Common.Admin.ViewModels
{
    public class UsersConciseViewModel
    {
        
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public string City { get; set; }
        
        public DateTime RegisteredDate { get; set; }
        public bool isLock { get; set; }
        public bool IsAdmin { get; set; }

        public int Ads { get; set; }
    }
}