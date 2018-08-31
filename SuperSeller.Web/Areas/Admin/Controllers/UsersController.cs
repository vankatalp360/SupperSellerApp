using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperSeller.Common.Admin.BindingModels;
using SuperSeller.Models;
using SuperSeller.Services.Admin.Interfaces;

namespace SuperSeller.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }


        public IActionResult Index()
        {
            var model = usersService.GetAllUsers().Result;

            return View(model);
        }

        public IActionResult Details(string id)
        {
            var model = usersService.GetUser(id).Result;

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangeUserPassword(string id)
        {
            var user = usersService.GetUser(id);
            var model = new ChangePasswordBindingModel()
            {
                Id = id,
                Username = user.Result.UserName
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeUserPassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["errorMessage"] = "Invalid input!";
                return View();
            }

            var isChange = usersService.ChangeUserPassword(model.Id, model.Password);

            if (!isChange.Result)
            {
                TempData["errorMessage"] = "Something goes wrong.";
            }

            TempData["successMessage"] = $"{model.Username}'s password has been changed successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Ban(string id)
        {
            var user = usersService.GetUser(id);
            TempData["Username"] = user.Result.UserName;
            return View();
        }

        [HttpPost]
        public IActionResult Ban(BanUserBindingModel model, string id)
        {
            var user = usersService.GetUser(id);
            var isLock = usersService.BanUser(id, model.EndDate);

            if (!isLock.Result)
            {
                this.TempData["errorMessage"] = $"{user.Result.UserName}'s account wasn't banned!";
            }
            this.TempData["successMessage"] = $"{user.Result.UserName}'s account was banned to {model.EndDate}";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Unban(string id)
        {
            var user = usersService.GetUser(id).Result;
            var isUnLock = usersService.UnbanUser(id);
            if (!isUnLock)
            {
                this.TempData["errorMessage"] = $"{user.UserName}'s account wasn't unlock!";
            }
            this.TempData["successMessage"] = $"{user.UserName}'s account was unlocked.";
            return RedirectToAction("Index");
        }
    }
}