using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SuperSeller.Common.Admin.ViewModels;

namespace SuperSeller.Services.Admin.Interfaces
{
    public interface IUsersService
    {
        Task<bool> IsAdmin(ClaimsPrincipal user);
        Task<ICollection<UsersConciseViewModel>> GetAllUsers();
        Task<UserViewModel> GetUser(string id);

        Task<bool> ChangeUserPassword(string userId, string password);
        Task<bool> BanUser(string userId, DateTime endDate);
        bool UnbanUser(string userId);
    }
}