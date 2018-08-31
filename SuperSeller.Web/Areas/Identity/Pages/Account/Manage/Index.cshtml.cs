using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperSeller.Models;
using SuperSeller.Services.Users.Interfaces;

namespace SuperSeller.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IUserService userService;

        public IndexModel(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.userService = userService;
        }

        public string Username { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get; set; }

        public string PicturePath { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Register Date")]
        public DateTime RegisterDate { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var userName = await userManager.GetUserNameAsync(user);
            var email = await userManager.GetEmailAsync(user);
            var phoneNumber = await userManager.GetPhoneNumberAsync(user);

            var region = user.Region;
            var address = user.Address;
            var city = user.City;
            var postalCode = user.PostalCode;

            PicturePath = user.ImagePath == null ? @"/images/Profile/avatar.jpg" : user.ImagePath;
            FullName = user.FullName;
            RegisterDate = user.RegisteredDate;
            Username = userName;

            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                Region = region,
                Address = address,
                City = city,
                PostalCode = postalCode,
                PicturePath = this.PicturePath
            };

            IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var email = await userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
            var region = user.Region;
            if (Input.Region != region)
            {
                await userService.ChangeRegion(user, Input.Region);
            }
            var address = user.Address;
            if (Input.Address != address)
            {
                await userService.ChangeAddress(user, Input.Address);
            }
            var city = user.City;
            if (Input.City != city)
            {
                await userService.ChangeCity(user, Input.City);
            }
            var postalCode = user.PostalCode;
            if (Input.PostalCode != postalCode)
            {
                await userService.ChangePostalcode(user, Input.PostalCode);
            }
            if (Input.ProfilePicture != null)
            {
                var picturePath = SavePicture(Input.ProfilePicture, Username).Result;
                await userService.UpdateProfilePicture(user, picturePath);
            }

            await signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }


            var userId = await userManager.GetUserIdAsync(user);
            var email = await userManager.GetEmailAsync(user);
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }

        public class InputModel
        {
            [DataType(DataType.Upload)]
            public IFormFile ProfilePicture { get; set; }

            public string PicturePath { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            
            public string Region { get; set; }

            public string City { get; set; }

            public string Address { get; set; }

            [DataType(DataType.PostalCode)]
            [DisplayName("Postal Code")]
            public string PostalCode { get; set; }
        }


        private async Task<string> SavePicture(IFormFile model, string username)
        {
            var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Profile", $"{model.FileName}");
            var fileStream = new FileStream(fullFilePath, FileMode.Create);
            await model.CopyToAsync(fileStream);
            var path = $"/images/Profile/{model.FileName}";
            return path;
        }
    }
}
