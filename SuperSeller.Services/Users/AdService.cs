using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperSeller.Common.Admin.BindingModels;
using SuperSeller.Common.Admin.ViewModels;
using SuperSeller.Common.User.BindingModels;
using SuperSeller.Common.User.ViewModels;
using SuperSeller.Data;
using SuperSeller.Models;
using SuperSeller.Services.Users.Interfaces;

namespace SuperSeller.Services.Users
{
    public class AdService : BaseEFService, IAdService
    {
        public AdService(ApplicationDbContext dbContext, IMapper mapper) 
            :base(dbContext, mapper)
        {
        }

        public async Task CreateAd(CreateAdBindingModel model, ICollection<string> picturesPaths)
        {
            var pictures = CreatePictures(picturesPaths);
            var ad = new Ad()
            {
                CategoryId = model.CategoryId,
                Title = model.Title,
                Price = model.Price,
                City = model.City,
                Email = model.Email,
                ConditionId = model.ConditionId,
                ContactNumber = model.ContactNumber,
                CreatorId = model.CreatorId,
                Description = model.Description,
                PostalCode = model.PostalCode,
                Region = model.Region,
                CreateDate = DateTime.Now,
                Pictures = pictures
            };

            DbContext.Ads.Add(ad);
            await DbContext.SaveChangesAsync();
        }

        public ICollection<CategoryAdViewModel> GetCattegories()
        {
            var dbCategories = DbContext.Categories.Include(c => c.SubCategories).ToList();
            var categories = Mapper.Map<ICollection<CategoryAdViewModel>>(dbCategories);
            return categories;
        }

        public ICollection<Condition> GetConditions()
        {
            var conditions = DbContext.Conditions.ToList();
            return conditions;
        }

        public AdsViewModel GetAdDetails(int id)
        {
            var dbAd = DbContext.Ads
                .Include(a => a.Category).ThenInclude(c => c.Category)
                .Include(a => a.Creator)
                .Include(a => a.Condition)
                .Include(a => a.Pictures)
                .FirstOrDefault(a => a.Id == id);
            if (dbAd == null)
            {
                return null;
            }
            dbAd.Viewing++;
            DbContext.SaveChanges();

            var ad = Mapper.Map<AdsViewModel>(dbAd);
            return ad;
        }

        public bool DeleteAd(int id, User user, bool isAdmin)
        {
            var adDb = DbContext.Ads.Find(id);
            if (adDb.CreatorId != user.Id && !isAdmin)
            {
                return false;
            }

            DbContext.Ads.Remove(adDb);
            DbContext.SaveChanges();
            return true;
        }

        public ICollection<AdsRangeViewModel> GetAds(string search)
        {
            if (search != null)
            {
                var dbAds = DbContext.Ads.Include(a => a.Pictures).Where(a => a.Title.Contains(search)).ToList();
                var ads = Mapper.Map<ICollection<AdsRangeViewModel>>(dbAds);
                return ads;
            }
            else
            {
                var dbAds = DbContext.Ads.Include(a => a.Pictures).ToList();
                var ads = Mapper.Map<ICollection<AdsRangeViewModel>>(dbAds);
                return ads;
            }
        }

        public ICollection<Picture> CreatePictures(ICollection<string> picturesPaths)
        {
            var pictures = new List<Picture>();

            foreach (var path in picturesPaths)
            {
                var picture = new Picture()
                {
                    Path = path
                };
                pictures.Add(picture);
            }
            return pictures;
        }
    }
}