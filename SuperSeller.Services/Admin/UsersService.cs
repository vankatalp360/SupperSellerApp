using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SuperSeller.Data;
using SuperSeller.Models;
using SuperSeller.Services.Admin.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SuperSeller.Common.Admin.ViewModels;

namespace SuperSeller.Services.Admin
{
    public class UsersService : BaseEFService, IUsersService
    {
        private readonly UserManager<User> userManager;

        public UsersService(ApplicationDbContext dbContext, IMapper mapper, UserManager<User> userManager) 
            :base(dbContext, mapper)
        {
            this.userManager = userManager;
        }

        public async Task<bool> IsAdmin(ClaimsPrincipal user)
        {
            var currentUser = await userManager.GetUserAsync(user);
            return await userManager.IsInRoleAsync(currentUser, "Administrator");
        }

        public async Task<ICollection<UsersConciseViewModel>> GetAllUsers()
        {
            var dbUsers = await DbContext.Users.Include(u => u.Ads).ToListAsync();
            var users = Mapper.Map<ICollection<UsersConciseViewModel>>(dbUsers);
            return users;
        }

        public async Task<UserViewModel> GetUser(string id)
        {
            var dbUser = await DbContext.Users.Include(u => u.Ads).ThenInclude(a => a.Condition).FirstOrDefaultAsync(u => u.Id == id);
            var user = Mapper.Map<UserViewModel>(dbUser);
            return user;
        }

        public async Task<bool> ChangeUserPassword(string userId, string password)
        {
            var user = DbContext.Users.Find(userId);
            if (user == null)
            {
                return false;
            }
            IdentityResult isChange;
            var isRemoveSucceeded = userManager.RemovePasswordAsync(user);
            if (isRemoveSucceeded.Result.Succeeded)
            {
                isChange = await userManager.AddPasswordAsync(user, password);
            }
            else
            {
                return false;
            }
            return isChange.Succeeded;
        }

        public async Task<bool> BanUser(string userId, DateTime endDate)
        {
            var user = DbContext.Users.Find(userId);
            if (user == null)
            {
                return false;
            }
            await userManager.SetLockoutEnabledAsync(user, true);
            var isLock = await userManager.SetLockoutEndDateAsync(user, endDate);
            
            return isLock.Succeeded;
        }

        public bool UnbanUser(string userId)
        {
            var user = DbContext.Users.Find(userId);
            if (user == null)
            {
                return false;
            }
            var isUnLock = userManager.SetLockoutEnabledAsync(user, false).Result;

            return isUnLock.Succeeded;
        }
    }
}