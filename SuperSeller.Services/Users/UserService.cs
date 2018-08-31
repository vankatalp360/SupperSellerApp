using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SuperSeller.Data;
using SuperSeller.Models;
using SuperSeller.Services.Users.Interfaces;

namespace SuperSeller.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task ChangeRegion(User user, string region)
        {
            var dbUser = context.Users.Find(user.Id);
            dbUser.Region = region;
            await context.SaveChangesAsync();
        }

        public async Task UpdateProfilePicture(User user, string path)
        {
            var dbUser = context.Users.Find(user.Id);
            dbUser.ImagePath = path;
            await context.SaveChangesAsync();
        }
        public async Task ChangeAddress(User user, string address)
        {
            var dbUser = context.Users.Find(user.Id);
            dbUser.Address = address;
            await context.SaveChangesAsync();
        }
        public async Task ChangeCity(User user, string city)
        {
            var dbUser = context.Users.Find(user.Id);
            dbUser.City = city;
            await context.SaveChangesAsync();
        }
        public async Task ChangePostalcode(User user, string postalCode)
        {
            var dbUser = context.Users.Find(user.Id);
            dbUser.PostalCode = postalCode;
            await context.SaveChangesAsync();
        }
    }
}