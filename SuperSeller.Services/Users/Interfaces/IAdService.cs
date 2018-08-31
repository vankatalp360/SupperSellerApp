using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSeller.Common.Admin.ViewModels;
using SuperSeller.Common.User.BindingModels;
using SuperSeller.Common.User.ViewModels;
using SuperSeller.Models;

namespace SuperSeller.Services.Users.Interfaces
{
    public interface IAdService
    {
        ICollection<CategoryAdViewModel> GetCattegories();
        ICollection<Condition> GetConditions();
        ICollection<Picture> CreatePictures(ICollection<string> picturesPaths);
        Task CreateAd(CreateAdBindingModel model, ICollection<string> picturesPaths);
        AdsViewModel GetAdDetails(int id);
        ICollection<AdsRangeViewModel> GetAds(string search);

        bool DeleteAd(int id, User user, bool isAdmin);
    }
}