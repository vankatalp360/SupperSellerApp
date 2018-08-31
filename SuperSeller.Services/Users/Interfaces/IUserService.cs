using System.Threading.Tasks;
using SuperSeller.Data;
using SuperSeller.Models;

namespace SuperSeller.Services.Users.Interfaces
{
    public interface IUserService
    {
        Task ChangeRegion(User user, string region);

        Task ChangeAddress(User user, string address);

        Task ChangeCity(User user, string city);

        Task ChangePostalcode(User user, string postalCode);
        Task UpdateProfilePicture(User user, string path);
    }
}